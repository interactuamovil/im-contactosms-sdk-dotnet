using System;
using System.Collections.Generic;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api
{
    public class ResponseObjects
    {
        public class ApiResponse<T>
        {
            public String response { get; set; }
            public T data { get; set; }
            public System.Net.HttpStatusCode httpCode { get; set; }
            public String httpDescription { get; set; }
            public Int32 errorCode { get; set; }
            public String errorDescription { get; set; }

            public ApiResponse()
            {
                response = String.Empty;
                data = default(T);
                errorCode = Convert.ToInt32(System.Net.HttpStatusCode.OK);
                errorDescription = System.Net.HttpStatusCode.OK.ToString();
            }

            public Boolean isOk
            {
                get { return (httpCode == System.Net.HttpStatusCode.OK); }
            }
        }

        public class ErrorResponse
        {
            public Int32 code { get; set; }
            public String error { get; set; }
        }

        public class AccountStatusResponse
        {
            public String name { get; set; }
            public String sms_short_name { get; set; }
            public String sms_subscription_type { get; set; }
            public String sms_optin_keyword { get; set; }
            public Int32 messages_limit { get; set; }
            public Int32 messages_sent { get; set; }            
        }

        public class ContactResponse
        {
            public String msisdn { get; set; }
            public String first_name { get; set; }
            public String last_name { get; set; }
            public String status { get; set; }
        }

        public class ActionMessageResponse
        {
            public String result { get; set; }
        }

        public class GroupResponse
        {
            public String short_name { get; set; }
            public String name { get; set; }
            public String description { get; set; }
            public GroupMembers members { get; set; }
        }

        public class GroupMembers
        {
            public String total { get; set; }
            public String pending { get; set; }
            public String confirmed { get; set; }
        }

        public class MessageResponse
        {
            public Int32 id { get; set; }
            public String username { get; set; }
            public String sent_on { get; set; }
            public String message { get; set; }
            public Int32 recipients_count { get; set; }
            public List<String> recipients { get; set; }
            public List<String> groups { get; set; }
        }

        public class MessageRecipients
        {
            public String msisdn { get; set; }
        }

        public class InboxMessageResponse
        {
            public String status { get; set; }
            public String msisdn { get; set; }
            public String datetime { get; set; }
            public String message { get; set; }
            public String message_id { get; set; }
            public String short_number { get; set; }

            public String created_on { get; set; }
            public Int32  is_deleted { get; set; }
        }

        public class MessageToGroupResponse
        {
            public Int32 sms_sent { get; set; }
            public String sms_message { get; set; }
        }

        public class ScheduleMessageResponse
        {
            public Int32 id { get; set; }
            public String name { get; set; }
            public String frequency { get; set; }
            public String message { get; set; }
            public DateTime date_expires { get; set; }
            public List<MessageGroup> groups { get; set; }
        }

        public class MessageGroup
        {
            public String short_name{ get; set; }
        }
    }
}
