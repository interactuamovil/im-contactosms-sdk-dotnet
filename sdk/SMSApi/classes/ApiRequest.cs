using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace InteractuaMovil.ContactoSms.Api
{
    internal class ApiRequest : ResponseObjects, IApiRequest
    {
        private string _BaseUrl;
        private string _ApiKey;
        private string _SecretKey;

        public enum request { get, post, put, delete }

        public ApiRequest(string ApiKey, string SecretKey, string ApiUrl) : base()
        {
            this.ApiKey = ApiKey;
            this.SecretKey = SecretKey;
            this._BaseUrl = ApiUrl;
        }

        public string BaseUrl
        {
            get{return _BaseUrl;}
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

        public object RequestToApi(string Url, request RType, Dictionary<string, string> UrlParams = null, Dictionary<string, dynamic> BodyParams = null, bool AddToQueryString = false)
        {
            string data = "", filters = "";
            var jss = new JavaScriptSerializer();
            string date = DateTime.Now.ToString("r");
            string cannonical, b64mac, hash;
            Dictionary<string, string> headers = new Dictionary<string,string>();

            if (BodyParams != null)
                if (BodyParams.Count > 0)
                    data = JsonConvert.SerializeObject(BodyParams);
                    //data = jss.Serialize(BodyParams);

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

            return SendRequest(Url, headers, RType, data);
        }

        public object SendRequest(string Url, Dictionary<string, string> Headers, request RType, string BodyParams=null)
        {
            Url = BaseUrl + Url;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Url);
                byte[] byteArray = Encoding.UTF8.GetBytes(BodyParams);
                string requestType = this.GetRequestType(RType);
                var jss = new JavaScriptSerializer();
            
                request.Method = requestType;
                if (BodyParams.Length > 2)
                    request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentLength = byteArray.Length;
                foreach (string key in Headers.Keys)
                    if (key != "Date")
                        request.Headers.Add(key, Headers[key].ToString());
                request.Date = Convert.ToDateTime(Headers["Date"]);
            
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                int responseCode = (int)(((HttpWebResponse)response).StatusCode);
                if (responseCode == 403)
                    throw new System.ArgumentException("Invalid authentication");
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string serverResponse = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                
                //return jss.DeserializeObject(serverResponse);
                return serverResponse;
            }
            catch (Exception e)
            {
                List<string> error = new List<string>();
                error.Add("Error|" + e.Message);
                return error;
            }
        }

        private string GetRequestType(request RType)
        {
            string requestType = "";
            switch (RType)
            {
                case ApiRequest.request.get:
                    requestType = "HTTPGET";
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
                var orderParameters = Parameters.Keys.ToList();
                orderParameters.Sort();
                foreach (var key in orderParameters)
                {
                    if (!firstParam)
                        queryString += "&";
                    queryString += key + "=" + Parameters[key];
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

    }
}
