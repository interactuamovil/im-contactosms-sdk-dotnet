using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace InteractuaMovil.ContactoSms.Api
{
    internal class Messages : ApiRequest, interfaces.IMessages
    {

        internal Messages(string ApiKey, string SecretKey, string ApiUrl) : base(ApiKey, SecretKey, ApiUrl) { }
        internal Messages(string ApiKey, string SecretKey, string ApiUrl, string ProxyAddress, string UserName, string Password) : base(ApiKey, SecretKey, ApiUrl, ProxyAddress, UserName, Password) { }

        /// <summary>
        /// Gets the messages log
        /// </summary>
        /// <param name="StartDate">FROM this date</param>
        /// <param name="EndDate">TO this date</param>
        /// <param name="Start">From this message on the list</param>
        /// <param name="Limit">Maximun number of records</param>
        /// <param name="Msisdn">Country code and phone number</param>
        /// <param name="ShortName">Filter all messages sent to specified group's short_name</param>
        /// <param name="Include_Recipients">If true it will include the list of recipients for each message</param>
        /// <returns>Object with the message list</returns>
        public ResponseObjects.ApiResponse<List<MessageResponse>> GetList(DateTime? StartDate = null, DateTime? EndDate = null, Int32 Start = -1, Int32 Limit = -1, String Msisdn = null, String ShortName = null, Boolean IncludeRecipients = false)
        { 
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            
            if( StartDate.HasValue)
            {
                UrlParameters.Add("start_date", StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (EndDate.HasValue)
            {
                UrlParameters.Add("end_date", EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if ( Start != -1)
                UrlParameters.Add("start", Start.ToString());
            if ( Limit != -1)
                UrlParameters.Add("limit", Limit.ToString());
            if ( Msisdn != null)
                UrlParameters.Add("msisdn", Msisdn);
            if ( ShortName != null)
                UrlParameters.Add("short_name", ShortName);
            UrlParameters.Add("include_recipients", IncludeRecipients.ToString().ToLower());

            ResponseObjects.ApiResponse<List<MessageResponse>> serverResponse = this.RequestToApi<List<MessageResponse>>("messages", request.get, UrlParameters, null, true);
            return serverResponse;

            //object serverResponse = this.RequestToApi("messages", request.get, UrlParameters, null, true);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<List<MessageResponse>>((string)serverResponse);
        }

        /// <summary>
        /// Sends a message to one or more groups
        /// </summary>
        /// <param name="ShortName">String array with the group's short names</param>
        /// <param name="Message">Messages content</param>
        /// <returns>Object with message and account information</returns>
        public ResponseObjects.ApiResponse<MessageToGroupResponse> SendToGroups(String[] ShortName, String Message)
        { 
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
			Parameters.Add("groups", ShortName);
			Parameters.Add("message", Message);

            ResponseObjects.ApiResponse<MessageToGroupResponse> serverResponse = this.RequestToApi<MessageToGroupResponse>("messages/send", request.post, null, Parameters);
            return serverResponse;

            //object serverResponse = this.RequestToApi("messages/send", request.post, null, Parameters);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<MessageToGroupResponse>((string)serverResponse);
        }

        /// <summary>
        /// Sends a message to a specific contact
        /// </summary>
        /// <param name="Msisdn">country code and phone number</param>
        /// <param name="Message">Messages content</param>
        /// <returns>Object with message info</returns>
        public ResponseObjects.ApiResponse<MessageToGroupResponse> SendToContact(String Msisdn, String Message) 
        {
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();
            Parameters.Add("msisdn", Msisdn);
            Parameters.Add("message", Message);

            ResponseObjects.ApiResponse<MessageToGroupResponse> serverResponse = this.RequestToApi<MessageToGroupResponse>("messages/send_to_contact", request.post, null, Parameters);
            return serverResponse;

            //object serverResponse = this.RequestToApi("messages/send_to_contact", request.post, null, Parameters);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<MessageToGroupResponse>((string)serverResponse);
        }

        /// <summary>
        /// Gets all the active schedule messages
        /// </summary>
        /// <returns></returns>
        public ResponseObjects.ApiResponse<List<ScheduleMessageResponse>> GetSchedule() 
        {
            ResponseObjects.ApiResponse<List<ScheduleMessageResponse>> serverResponse = this.RequestToApi<List<ScheduleMessageResponse>>("messages/scheduled", request.get, null, null);
            return serverResponse;
            //object serverResponse = this.RequestToApi("messages/scheduled", request.get, null, null);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<List<ScheduleMessageResponse>>((string)serverResponse);
        }

        /// <summary>
        /// Deletes a schedule message
        /// </summary>
        /// <param name="MessageId">Message ID to delete</param>
        /// <returns>Object with result message</returns>
        public ResponseObjects.ApiResponse<ActionMessageResponse> RemoveSchedule(String MessageId) 
        { 
            Dictionary<string, dynamic> Parameters = new Dictionary<string,dynamic>();
            Parameters.Add("message_id", MessageId);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("messages/scheduled", request.delete, null, Parameters);
            return serverResponse;
            //object serverResponse = this.RequestToApi("messages/scheduled", request.delete, null, Parameters);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Adds a new schedule messsage
        /// </summary>
        /// <param name="StartDate">Start sending messages FROM this date</param>
        /// <param name="EndDate">Send messages UNTIL this date</param>
        /// <param name="Name">Schedule message's name</param>
        /// <param name="Message">Message content</param>
        /// <param name="Time">Hour to send the message Ex. 09.00</param>
        /// <param name="Frequency">How often send the message Ex. ONCE</param>
        /// <param name="Groups">String array with the groups' short names where to send to the message</param>
        /// <returns>Object with schedule message info</returns>
        public ResponseObjects.ApiResponse<ActionMessageResponse> AddSchedule(DateTime StartDate, DateTime EndDate, String Name, String Message, String Time, String Frequency, String[] Groups)
        {
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();

            Parameters.Add("start_date", StartDate.ToString("yyyy-MM-dd"));
            Parameters.Add("end_date", EndDate.ToString("yyyy-MM-dd"));
            Parameters.Add("name", Name);
            Parameters.Add("message", Message);
            Parameters.Add("time", Time);
            Parameters.Add("frequency", Frequency.ToUpper());
            Parameters.Add("groups", Groups);

            ResponseObjects.ApiResponse<ActionMessageResponse> serverResponse = this.RequestToApi<ActionMessageResponse>("messages/scheduled", request.post, null, Parameters);
            return serverResponse;
            //object serverResponse = this.RequestToApi("messages/scheduled", request.post, null, Parameters);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
        }

        /// <summary>
        /// Gets the inbox messages
        /// </summary>
        /// <param name="StartDate">From this date</param>
        /// <param name="EndDate">To this date</param>
        /// <param name="Start">From this record (ordinay to start with)</param>
        /// <param name="Limit">Maximun amount of records to retrieve</param>
        /// <param name="Msisdn">country code and phone number</param>
        /// <param name="Status">0 =PENDING, 1 = CONFIRMED, 2 =  CANCELLED</param>
        /// <returns>Object with the messages list</returns>
        public ResponseObjects.ApiResponse<List<InboxMessageResponse>> Inbox(DateTime? StartDate = null, DateTime? EndDate = null, Int32 Start = -1, Int32 Limit = -1, String Msisdn = null, Int32 Status = -1)
        {
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();

            if ( StartDate.HasValue && EndDate.HasValue )
            {
                UrlParameters.Add("start_date", StartDate.Value.ToString("yyy-MM-dd"));
                UrlParameters.Add("end_date", EndDate.Value.ToString("yyyy-MM-dd"));
            }
            if ( Start != -1 )
                UrlParameters.Add("start", Start.ToString());
            if ( Limit != -1 )
                UrlParameters.Add("limit", Limit.ToString());
            if ( Msisdn != null )
                UrlParameters.Add("msisdn", Msisdn);
            if ( Status != -1 )
                UrlParameters.Add("status", Status.ToString());

            ResponseObjects.ApiResponse<List<InboxMessageResponse>> serverResponse = this.RequestToApi<List<InboxMessageResponse>>("messages/inbox", request.get, UrlParameters, null, true);
            return serverResponse;
            //object serverResponse = this.RequestToApi("messages/messages_inbox", request.get, UrlParameters, null, true);
            //object serverResponse = this.RequestToApi("messages/inbox", request.get, UrlParameters, null, true);
            //if (serverResponse.GetType() == typeof(List<string>))
            //    return serverResponse;
            //else
            //    return JsonConvert.DeserializeObject<List<InboxMessageResponse>>((string)serverResponse);
        }
    }
}
