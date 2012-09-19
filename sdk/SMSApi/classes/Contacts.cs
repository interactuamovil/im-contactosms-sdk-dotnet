using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace InteractuaMovil.ContactoSms.Api
{
    internal class Contacts : ApiRequest, interfaces.IContacts
    {
        internal Contacts(string ApiKey, string SecretKey, string ApiUrl) : base (ApiKey, SecretKey, ApiUrl) { }

        /// <summary>
        /// Gets a contact list
        /// </summary>
        /// <param name="Start">Starts from the contact # in the list</param>
        /// <param name="Limit">Contacts amount limit</param>
        /// <param name="FirstName">Search for contacts with name like</param>
        /// <param name="LastName">Search for contacts with last name like</param>
        /// <param name="Status">0 =PENDING, 1 = CONFIRMED, 2 =  CANCELLED</param>
        /// <returns>Object array with the contacts list</returns>
        public object GetList(int Start=-1, int Limit=-1, string FirstName=null, string LastName=null, int Status=-1)
        {
            Dictionary<string, string> Parameters = new Dictionary<string,string>();
            if (Start != -1)
                Parameters.Add("start", Start.ToString());
            if (Limit != -1)
                Parameters.Add("limit", Limit.ToString());
            if (FirstName != null)
                Parameters.Add("first_name", FirstName);
            if (LastName != null)
                Parameters.Add("last_name", LastName);
            if (Status != -1)
                Parameters.Add("status", Status.ToString());

            object serverResponse = this.RequestToApi("contacts", request.get, Parameters, null, true);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<List<ContactResponse>>((string)serverResponse);
        }

        /// <summary>
        /// Gets a contact by its msisdn
        /// </summary>
        /// <param name="Msisdn">Country code + phone number</param>
        /// <returns>object with the contact information</returns>
        public object GetByMsisdn(string Msisdn)
        {
            Dictionary<string, string> Parameters = new Dictionary<string, string>();
            Parameters.Add("msisdn", Msisdn);
            object serverResponse = this.RequestToApi("contacts/" + Msisdn, request.get, Parameters, null);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<ContactResponse>((string)serverResponse);
        }

        /// <summary>
        /// Updates a contact base on its msisdn
        /// </summary>
        /// <param name="Msisdn">Country code + phone number</param>
        /// <param name="FirstName">Updated contacts first name</param>
        /// <param name="LastName">Updated contacts last name</param>        
        /// <param name="NewMsisdn">msisdn to update the current one</param>
        /// <returns>Object with the result message</returns>
        public object Update(string Msisdn, string FirstName = null, string LastName = null, string NewMsisdn = null)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();

            UrlParameters.Add("msisdn", Msisdn);

            if (NewMsisdn != null)
                Parameters.Add("msisdn", NewMsisdn.ToString());
            if (FirstName != null)
                Parameters.Add("first_name", FirstName);
            if (LastName != null)
                Parameters.Add("last_name", LastName);            

            object serverResponse = this.RequestToApi("contacts/" + Msisdn, request.put, UrlParameters, Parameters);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Adds a new contact
        /// </summary>
        /// <param name="Msisdn">country code + phone number</param>
        /// <param name="FirstName">contacts first name</param>
        /// <param name="LastName">contacts last name</param>        
        /// <returns></returns>
        public object Add(string Msisdn, string FirstName, string LastName)
        {
           Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
           Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();
           
           UrlParameters.Add("msisdn", Msisdn);

           if (Msisdn != null)
               Parameters.Add("msisdn", Msisdn.ToString());
           if (FirstName != null)
               Parameters.Add("first_name", FirstName);
           if (LastName != null)
               Parameters.Add("last_name", LastName);           

           object serverResponse = this.RequestToApi("contacts/" + Msisdn, request.post, UrlParameters, Parameters);
           if (serverResponse.GetType() == typeof(List<string>))
               return serverResponse;
           else
               return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Deletes a contact
        /// </summary>
        /// <param name="Msisdn">country code + phone number</param>
        /// <returns>Object with result message</returns>
        public object Delete(string Msisdn) 
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("msisdn", Msisdn);

            object serverResponse = this.RequestToApi("contacts/" + Msisdn, request.delete, UrlParameters, null);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Gets contact's group list
        /// </summary>
        /// <param name="Msisdn">Contact's country code + phone number</param>
        /// <returns>object with the group list</returns>
        public object GetGroupList(string Msisdn)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            UrlParameters.Add("msisdn", Msisdn);

            object serverResponse = this.RequestToApi("contacts/" + Msisdn + "/groups", request.get, UrlParameters, null);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<List<GroupResponse>>((string)serverResponse);
        }
        
    }
}
