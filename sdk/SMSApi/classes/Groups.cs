using System;
using System.Collections.Generic;
using System.Linq;
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
            ResponseObjects.ApiResponse<List<GroupResponse>> serverResponse = this.RequestToApi<List<GroupResponse>>("groups", request.get, null, null);
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
            UrlParameters.Add("short_name", ShortName);

            ResponseObjects.ApiResponse<GroupResponse> serverResponse = this.RequestToApi<GroupResponse>("groups/" + ShortName, request.get, UrlParameters, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName, request.get, UrlParameters, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<GroupResponse>((string)serverResponse);
        }

        /// <summary>
        /// Updates a group
        /// </summary>
        /// <param name="ShortName">Group's short name to locate the group</param>
        /// <param name="Name">Group name</param>
        /// <param name="Description">Group Description</param>
        /// <param name="NewShortName">New group short name</param>
        /// <returns></returns>
        public ResponseObjects.ApiResponse<ActionMessageResponse> Update(string ShortName, string Name, string Description, string NewShortName = null)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();

            UrlParameters.Add("short_name", ShortName);

            Parameters.Add("name", Name);
            Parameters.Add("description", Description);
            if (NewShortName != null)
                Parameters.Add("short_name", NewShortName);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("groups/" + ShortName, request.put, UrlParameters, Parameters);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName, request.put, UrlParameters, Parameters);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Creates a new group
        /// </summary>
        /// <param name="ShortName">Group short name</param>
        /// <param name="Name">Group name</param>
        /// <param name="Description">Group description</param>
        /// <returns></returns>
        public ResponseObjects.ApiResponse<ActionMessageResponse> Add(string ShortName, string Name, string Description)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();

            UrlParameters.Add("short_name", ShortName);

            Parameters.Add("name", Name);
            Parameters.Add("description", Description);
            Parameters.Add("short_name", ShortName);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("groups/" + ShortName, request.post, UrlParameters, Parameters);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName, request.post, UrlParameters, Parameters);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Deletes a group
        /// </summary>
        /// <param name="ShortName">Group short name</param>
        /// <returns></returns>
        public ResponseObjects.ApiResponse<ActionMessageResponse> Delete(string ShortName)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("short_name", ShortName);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("groups/" + ShortName, request.delete, UrlParameters, null);
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
        public ResponseObjects.ApiResponse<List<ContactResponse>> GetContactList(string ShortName)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("short_name", ShortName);

            ResponseObjects.ApiResponse<List<ContactResponse>> serverResponse = this.RequestToApi<List<ContactResponse>>("groups/" + ShortName + "/contacts", request.get, UrlParameters, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName + "/contacts", request.get, UrlParameters, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<List<ContactResponse>>((string)serverResponse);
        }

        /// <summary>
        /// Add a contact to a group
        /// </summary>
        /// <param name="ShortName">group short name</param>
        /// <param name="Msisdn">contact msisdn</param>
        /// <returns></returns>
        public ResponseObjects.ApiResponse<ActionMessageResponse> AddContact(string ShortName, string Msisdn)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("msisdn", Msisdn);
            UrlParameters.Add("short_name", ShortName);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("groups/" + ShortName + "/contacts/" + Msisdn, request.post, UrlParameters, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName + "/contacts/" + Msisdn, request.post, UrlParameters, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Removes a contact from a group
        /// </summary>
        /// <param name="ShortName">group short name</param>
        /// <param name="Msisdn">contact msisdn</param>
        /// <returns></returns>
        public ResponseObjects.ApiResponse<ActionMessageResponse> RemoveContact(string ShortName, string Msisdn)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("msisdn", Msisdn);
            UrlParameters.Add("short_name", ShortName);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("groups/" + ShortName + "/contacts/" + Msisdn, request.delete, UrlParameters, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("groups/" + ShortName + "/contacts/" + Msisdn, request.delete, UrlParameters, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }
    }
}
