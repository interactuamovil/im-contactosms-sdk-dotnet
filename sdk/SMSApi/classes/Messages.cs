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

        /// <summary>
        /// Gets the messages log
        /// </summary>
        /// <param name="StartDate">FROM this date</param>
        /// <param name="EndDate">TO this date</param>
        /// <param name="Start">From this message on the list</param>
        /// <param name="Limit">Maximun number of records</param>
        /// <param name="Msisdn">country code and phone number</param>
        /// <returns>Object with the message list</returns>
        public object GetList(DateTime? StartDate = null, DateTime? EndDate = null, int Start=-1, int Limit=-1, string Msisdn=null)
        { 
            Dictionary<string, string> UrlParameters = new Dictionary<string, string>();
            
            if( StartDate.HasValue && EndDate.HasValue )
            {
                UrlParameters.Add("start_date", StartDate.Value.ToString("yyyy-MM-dd"));
                UrlParameters.Add("end_date", EndDate.Value.ToString("yyyy-MM-dd"));
            }
            if ( Start != -1)
                UrlParameters.Add("start", Start.ToString());
            if ( Limit != -1)
                UrlParameters.Add("limit", Limit.ToString());
            if ( Msisdn != null)
                UrlParameters.Add("msisdn", Msisdn);

            object serverResponse = this.RequestToApi("messages", request.get, UrlParameters, null, true);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<List<MessageResponse>>((string)serverResponse);
        }

        /// <summary>
        /// Sends a message to one or more groups
        /// </summary>
        /// <param name="ShortName">String array with the group's short names</param>
        /// <param name="Message">Messages content</param>
        /// <returns>Object with message and account information</returns>
        public object SendToGroups (string[] ShortName, string Message)
        { 
            Dictionary<string, object> Parameters = new Dictionary<string, object>();
			Parameters.Add("groups", ShortName);
			Parameters.Add("message", Message);

            object serverResponse = this.RequestToApi("messages/send", request.post, null, Parameters);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<MessageToGroupResponse>((string)serverResponse);
        }

        /// <summary>
        /// Sends a message to a specific contact
        /// </summary>
        /// <param name="Msisdn">country code and phone number</param>
        /// <param name="Message">Messages content</param>
        /// <returns>Object with message info</returns>
        public object SendToContact (string Msisdn, string Message) 
        {
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();
            Parameters.Add("msisdn", Msisdn);
            Parameters.Add("message", Message);

            object serverResponse = this.RequestToApi("messages/send_to_contact", request.post, null, Parameters);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<MessageToGroupResponse>((string)serverResponse);
        }

        /// <summary>
        /// Gets all the active schedule messages
        /// </summary>
        /// <returns></returns>
        public object GetSchedule () 
        {
            object serverResponse = this.RequestToApi("messages/scheduled", request.get, null, null);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<List<ScheduleMessageResponse>>((string)serverResponse);
        }

        /// <summary>
        /// Deletes a schedule message
        /// </summary>
        /// <param name="MessageId">Message ID to delete</param>
        /// <returns>Object with result message</returns>
        public object RemoveSchedule (string MessageId) 
        { 
            Dictionary<string, dynamic> Parameters = new Dictionary<string,dynamic>();
            Parameters.Add("message_id", MessageId);

            object serverResponse = this.RequestToApi("messages/scheduled", request.delete, null, Parameters);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
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
        public object AddSchedule (DateTime StartDate, DateTime EndDate, string Name, string Message, string Time, string Frequency, string[] Groups)
        {
            Dictionary<string, dynamic> Parameters = new Dictionary<string, dynamic>();

            Parameters.Add("start_date", StartDate.ToString("yyyy-MM-dd"));
            Parameters.Add("end_date", EndDate.ToString("yyyy-MM-dd"));
            Parameters.Add("name", Name);
            Parameters.Add("message", Message);
            Parameters.Add("time", Time);
            Parameters.Add("frequency", Frequency.ToUpper());
            Parameters.Add("groups", Groups);

            object serverResponse = this.RequestToApi("messages/scheduled", request.post, null, Parameters);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<ActionMessageResponse>((string)serverResponse);
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
        public object Inbox (DateTime? StartDate=null, DateTime? EndDate=null, int Start=-1, int Limit=-1, string Msisdn=null,  int Status=-1)
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

            object serverResponse = this.RequestToApi("messages/messages_inbox", request.get, UrlParameters, null, true);
            if (serverResponse.GetType() == typeof(List<string>))
                return serverResponse;
            else
                return JsonConvert.DeserializeObject<List<InboxMessageResponse>>((string)serverResponse);
        }
    }
}
