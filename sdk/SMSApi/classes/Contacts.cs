using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InteractuaMovil.ContactoSms.Api
{
    public enum ContactStatus
    {
        SUSCRIBED,
        CONFIRMED,
        CANCELLED,
        INVITED
    }

    public enum AddedFrom
    {
        WEB_FORM,
        FILE_UPLOAD,
        API,
        SUBSCRIPTION_REQUEST,
        SMS
    }

    internal class Contacts : ApiRequest, interfaces.IContacts
    {
        //private Object BodyObject;
        internal Contacts(string ApiKey, string SecretKey, string ApiUrl) : base (ApiKey, SecretKey, ApiUrl) { }
        internal Contacts(string ApiKey, string SecretKey, string ApiUrl, string ProxyAddress, string UserName, string Password) : base(ApiKey, SecretKey, SecretKey, ProxyAddress, UserName, Password) { }

        /// <summary>
        /// Gets a contact list
        /// </summary>
        /// <param name="Start">Starts from the contact # in the list</param>
        /// <param name="Limit">Contacts amount limit</param>
        /// <param name="Query">Search for contacts with 'query' in the first name, last name or msisdn</param>
        /// <param name="contactStatuses">a combination of SUSCRIBED, CONFIRMED, CANCELLED, INVITED. Defaults to SUSCRIBED,CONFIRMED</param>
        /// <param name="shortResults">Fetch only msisdn, first and last name</param>
        /// <returns>Object array with the contacts list</returns>
        public ResponseObjects.ApiResponse<List<ContactJson>> GetList(List<ContactStatus> contactStatuses = null, String query = null, int start = -1, int limit = -1, bool shortResults = false)
        {
            Dictionary<string, string> Parameters = new Dictionary<string,string>();
            if (contactStatuses != null && contactStatuses.Count > 0)
                Parameters.Add("status", String.Join(",",contactStatuses));
            if (query != null)
                Parameters.Add("query", query);
            if (start != -1)
                Parameters.Add("start", start.ToString());
            if (limit != -1)
                Parameters.Add("limit", limit.ToString());
            if (shortResults)
                Parameters.Add("short_results", "true");
            
            ResponseObjects.ApiResponse<List<ContactJson>> serverResponse = this.RequestToApi<List<ContactJson>>("contacts", request.get, Parameters, null, true);
            return serverResponse;
        }

        /// <summary>
        /// Gets a contact by its msisdn
        /// </summary>
        /// <param name="Msisdn">Country code + phone number</param>
        /// <returns>object with the contact information</returns>
        public ResponseObjects.ApiResponse<ContactJson> GetByMsisdn(string msisdn)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();
            Parameters.Add("msisdn", msisdn);

            ResponseObjects.ApiResponse<ContactJson> serverResponse = this.RequestToApi<ContactJson>("contacts/" + msisdn, request.get, Parameters, null);
            return serverResponse;
        }

        /// <summary>
        /// Updates a contact base on its msisdn
        /// </summary>
        /// <param name="Msisdn">Country code + phone number</param>
        /// <param name="FirstName">Updated contacts first name</param>
        /// <param name="LastName">Updated contacts last name</param>        
        /// <param name="NewMsisdn">msisdn to update the current one</param>
        /// <returns>Object with the result message</returns>
        public ResponseObjects.ApiResponse<ResponseObjects.ContactJson> Update(string countryCode, string msisdn, string firstName = null, string lastName = null)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();

            UrlParameters.Add("msisdn", msisdn);

            Parameters.Add("msisdn", msisdn.ToString());
            if (firstName != null)
                Parameters.Add("first_name", firstName);
            if (lastName != null)
                Parameters.Add("last_name", lastName);

            ResponseObjects.ApiResponse<ResponseObjects.ContactJson> serverResponse = this.RequestToApi<ResponseObjects.ContactJson>("contacts/" + msisdn, request.put, UrlParameters, Parameters);
            return serverResponse;
            //object serverResponse = this.RequestToApi("contacts/" + Msisdn, request.put, UrlParameters, Parameters);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        public ResponseObjects.ApiResponse<ActionMessageResponse> Update(ContactJson contact)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();

            UrlParameters.Add("msisdn", contact.Msisdn);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("contacts/" + contact.Msisdn, request.put, UrlParameters, null, false, (Object)contact);
            return serverResponse;
        }

        /// <summary>
        /// Adds a new contact
        /// </summary>
        /// <param name="Msisdn">country code + phone number</param>
        /// <param name="FirstName">contacts first name</param>
        /// <param name="LastName">contacts last name</param>        
        /// <returns></returns>
        public ResponseObjects.ApiResponse<ResponseObjects.ContactJson> Add(string countryCode, string Msisdn, string FirstName = null, string LastName = null)
        {
           Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
           Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();
           
           UrlParameters.Add("msisdn", Msisdn);

           ContactJson contact = new ContactJson();
           contact.Msisdn = Msisdn;
           contact.FirstName = FirstName;
           contact.LastName = LastName;
           contact.CountryCode = countryCode;

           return Add(contact);
        }

        public ResponseObjects.ApiResponse<ResponseObjects.ContactJson> Add(ContactJson contact)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();

            UrlParameters.Add("msisdn", contact.Msisdn);

            ResponseObjects.ApiResponse<ResponseObjects.ContactJson> serverResponse = this.RequestToApi<ResponseObjects.ContactJson>("contacts/" + contact.Msisdn, request.post, UrlParameters, null, false, (Object)contact);
            return serverResponse;
        }

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="Msisdn">country code + phone number</param>
        /// <returns>Object with result message</returns>
        public ResponseObjects.ApiResponse<ResponseObjects.ContactJson> Delete(string msisdn)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("msisdn", msisdn);

            ResponseObjects.ApiResponse<ResponseObjects.ContactJson> serverResponse = this.RequestToApi<ResponseObjects.ContactJson>("contacts/" + msisdn, request.delete, UrlParameters, null);
            return serverResponse;
        }

        /// <summary>
        /// Gets contact's group list
        /// </summary>
        /// <param name="Msisdn">Contact's country code + phone number</param>
        /// <returns>object with the group list</returns>
        public ResponseObjects.ApiResponse<List<GroupResponse>> GetGroupList(string Msisdn)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("msisdn", Msisdn);

            ResponseObjects.ApiResponse<List<GroupResponse>> serverResponse = this.RequestToApi<List<GroupResponse>>("contacts/" + Msisdn + "/groups", request.get, UrlParameters, null);
            return serverResponse;
        }
        
    }
}
