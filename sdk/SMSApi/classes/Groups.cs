using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InteractuaMovil.ContactoSms.Api
{
    internal class Groups: ApiRequest, interfaces.IGroups
    {
        internal Groups(string ApiKey, string SecretKey, string ApiUrl) : base(ApiKey, SecretKey, ApiUrl) { }
        internal Groups(string ApiKey, string SecretKey, string ApiUrl, string ProxyAddress, string UserName, string Password) : base(ApiKey, SecretKey, ApiUrl, ProxyAddress, UserName, Password) { }

        /// <summary>
        /// Gets the list of groups for the account
        /// </summary>
        /// <returns>List of groups</returns>
        public ResponseObjects.ApiResponse<List<GroupResponse>> GetList()
        {
            ResponseObjects.ApiResponse<List<GroupResponse>> serverResponse = this.RequestToApi<List<GroupResponse>>("tags", request.get, null, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups", request.get, null, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<List<GroupResponse>>((string)serverResponse);
        }

        /// <summary>
        /// Gets a single group
        /// </summary>
        /// <param name="ShortName">Group's short name</param>
        /// <returns>List of groups</returns>
        public ResponseObjects.ApiResponse<GroupResponse> Get(string ShortName)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("tag_name", ShortName);

            ResponseObjects.ApiResponse<GroupResponse> serverResponse = this.RequestToApi<GroupResponse>("tags/" + ShortName, request.get, UrlParameters, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName, request.get, UrlParameters, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<GroupResponse>((string)serverResponse);
        }

        /// <summary>
        /// Deletes a group
        /// </summary>
        /// <param name="ShortName">Group short name</param>
        /// <returns></returns>
        public ResponseObjects.ApiResponse<ActionMessageResponse> Delete(string ShortName)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("tag_name", ShortName);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("tags/" + ShortName, request.delete, UrlParameters, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName, request.delete, UrlParameters, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Gets group's contacts list
        /// </summary>
        /// <param name="ShortName">group short name</param>
        /// <returns></returns>
        public ResponseObjects.ApiResponse<List<ContactJson>> GetContactList(string ShortName)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("tag_name", ShortName);

            ResponseObjects.ApiResponse<List<ContactJson>> serverResponse = this.RequestToApi<List<ContactJson>>("tags/" + ShortName + "/contacts", request.get, UrlParameters, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName + "/contacts", request.get, UrlParameters, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<List<ContactResponse>>((string)serverResponse);
        }
    }
}
