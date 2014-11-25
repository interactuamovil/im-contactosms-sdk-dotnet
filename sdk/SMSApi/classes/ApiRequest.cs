using System;
using System.Collections.Generic;
#if !NET20
using System.Linq;
#endif
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace InteractuaMovil.ContactoSms.Api
{
    internal class ApiRequest : ResponseObjects, IApiRequest
    {
        private string _BaseUrl;
        private string _ApiKey;
        private string _SecretKey;
        private WebProxy _Proxy;

        public enum request { get, post, put, delete }

        public ApiRequest(string ApiKey, string SecretKey, string ApiUrl)
            : base()
        {
            this.ApiKey = ApiKey;
            this.SecretKey = SecretKey;
            this._BaseUrl = ApiUrl;
        }

        public ApiRequest(string ApiKey, string SecretKey, string ApiUrl, string ProxyAddress, string UserName, string Password)
            : this(ApiKey, SecretKey, ApiUrl)
        {

            NetworkCredential credentials = new NetworkCredential(UserName, Password);

            _Proxy = new WebProxy(ProxyAddress);
            _Proxy.Credentials = credentials;

        }

        public string BaseUrl
        {
            get { return _BaseUrl; }
        }

        public string ApiKey
        {
            get { return _ApiKey; }
            set { _ApiKey = value; }
        }

        public string SecretKey
        {
            get { return _SecretKey; }
            set { _SecretKey = value; }
        }

        //public object RequestToApi(string Url, request RType, Dictionary<string, string> UrlParams = null, Dictionary<string, dynamic> BodyParams = null, bool AddToQueryString = false)
        public ApiResponse<T> RequestToApi<T>(string Url, request RType, Dictionary<string, string> UrlParams = null, Dictionary<string, Object> BodyParams = null, bool AddToQueryString = false, object BodyObject = null)
        {
            string data = "", filters = "";
            //var jss = new JavaScriptSerializer();
            string date = DateTime.Now.ToString("r");
            string cannonical, b64mac, hash;
            Dictionary<string, string> headers = new Dictionary<string, string>();

            if (BodyParams != null)
                if (BodyParams.Count > 0)
                    data = JsonConvert.SerializeObject(BodyParams);
            if (BodyObject != null)
                data = JsonConvert.SerializeObject(BodyObject);

            if (UrlParams != null)
                if (UrlParams.Count > 0)
                    filters = ToQueryString(UrlParams);

            if (RType == request.get)
                cannonical = ApiKey + date + filters;
            else
                cannonical = ApiKey + date + filters + data;

            b64mac = HashMac(cannonical);
            hash = "IM " + ApiKey + ":" + b64mac;

            headers.Add("Authorization", hash);
            headers.Add("Date", date);

            if (AddToQueryString && filters.Length > 0)
                Url += "?" + filters;

            //return SendRequest(Url, headers, RType, data);
            return SendRequest<T>(Url, headers, RType, data);
        }

        //public object SendRequest(string Url, Dictionary<string, string> Headers, request RType, string BodyParams = null)
        public ApiResponse<T> SendRequest<T>(string Url, Dictionary<string, string> Headers, request RType, string BodyParams = null)
        {
            ApiResponse<T> dataResponse = new ApiResponse<T>();

            Url = BaseUrl + Url;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Url);
                byte[] byteArray = Encoding.UTF8.GetBytes(BodyParams);
                string requestType = this.GetRequestType(RType);
                //var jss = new JavaScriptSerializer();

                if (_Proxy != null)
                {
                    request.Proxy = _Proxy;
                }


                request.Method = requestType;

                request.Headers.Add("X-IM-ORIGIN", "IM_SDK_DOTNET");
                foreach (string key in Headers.Keys)
                    if (key != "Date")
                        request.Headers.Add(key, Headers[key].ToString());
                    else
                    {
#if NET35 || NET20
                        request.Headers.Add("X-IM-DATE", Headers[key].ToString());
#endif
#if NET40
                        request.Date = DateTime.Parse(Headers["Date"], System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal);
#endif
                    }


                if (RType != ApiRequest.request.get)
                {
                    request.ContentLength = byteArray.Length;
                    if (BodyParams.Length > 2)
                        request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    
                }

                WebResponse response = request.GetResponse();
                dataResponse.HttpCode = ((HttpWebResponse)response).StatusCode;
                dataResponse.HttpDescription = ((HttpWebResponse)response).StatusDescription;

                if (dataResponse.HttpCode != System.Net.HttpStatusCode.OK)
                {
                    dataResponse.ErrorCode = (int)dataResponse.HttpCode;
                    dataResponse.ErrorDescription = dataResponse.HttpDescription;
                }

                Stream respDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(respDataStream);
                dataResponse.Response = reader.ReadToEnd();
                reader.Close();
                respDataStream.Close();
                        
                if (dataResponse.ErrorCode == 403)
                {
                    ResponseObjects.ErrorResponse errorResponse = JsonConvert.DeserializeObject<ResponseObjects.ErrorResponse>(dataResponse.Response);
                    dataResponse.ErrorDescription = errorResponse.error;
                    //dataResponse.errorDescription = "Invalid Authentication";
                }
                else
                {
                    dataResponse.Data = JsonConvert.DeserializeObject<T>(dataResponse.Response); 
                }

                response.Close();
                //return jss.DeserializeObject(serverResponse);
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    dataResponse.ErrorCode = -1;
                    dataResponse.ErrorDescription = e.Message;

                }
                else
                {

                    dataResponse.HttpCode = (System.Net.HttpStatusCode)Convert.ToInt32(e.Response.Headers["Status"]);
                    dataResponse.HttpDescription = e.Message;

                    Stream dataStream = e.Response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    dataResponse.Response = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();

                    if (dataResponse.Response != null)
                    {
                        ResponseObjects.ErrorResponse errorResponse = JsonConvert.DeserializeObject<ResponseObjects.ErrorResponse>(dataResponse.Response);
                        if (errorResponse.code != 0)
                        {
                            dataResponse.ErrorCode = errorResponse.code;
                        }
                        else
                        {
                            dataResponse.ErrorCode = (int)dataResponse.HttpCode;
                        }
                        dataResponse.ErrorDescription = errorResponse.error;
                    }
                }
            }
            catch (Exception e)
            {
                dataResponse.ErrorCode = -1;
                dataResponse.ErrorDescription = e.Message;
            }
            finally
            {
                
            }

            return dataResponse;
        }

        private string GetRequestType(request RType)
        {
            string requestType = "";
            switch (RType)
            {
                case ApiRequest.request.get:
                    requestType = "GET";
                    break;
                case ApiRequest.request.post:
                    requestType = "POST";
                    break;
                case ApiRequest.request.put:
                    requestType = "PUT";
                    break;
                case ApiRequest.request.delete:
                    requestType = "DELETE";
                    break;
            }
            return requestType;
        }

        private string ToQueryString(Dictionary<string, string> Parameters)
        {
            string queryString = "";
            bool firstParam = true;

            if (Parameters.Count > 0)
            {
#if NET20
                var orderParameters = new List<String>(Parameters.Keys);
                orderParameters.Sort();
#else
                var orderParameters = Parameters.Keys.ToList();
                orderParameters.Sort();
#endif
                foreach (var key in orderParameters)
                {
                    if (!firstParam)
                        queryString += "&";
                    queryString += key + "=" + UrlEncodeUpperCase(Parameters[key]);
                    firstParam = false;
                }
            }

            return queryString;
        }

        private string HashMac(string EncrypText)
        {
            HMAC hasher;
            Byte[] utf8EncodedString = Encoding.UTF8.GetBytes(EncrypText);

            hasher = HMACSHA1.Create();
            hasher.Key = Encoding.UTF8.GetBytes(SecretKey);

            Byte[] hashResult = hasher.ComputeHash(utf8EncodedString);

            return Convert.ToBase64String(hashResult);
        }

        static string UrlEncodeUpperCase(string value)
        {
            value = HttpUtility.UrlEncode(value);
            return Regex.Replace(value, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());
        }

    }
}
