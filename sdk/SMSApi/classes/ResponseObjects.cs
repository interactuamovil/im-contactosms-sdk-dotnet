using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api
{
    public class ResponseObjects
    {

        public class AccountStatusResponse
        {
            public string name { get; set; }
            public string sms_short_name { get; set; }
            public string sms_subscription_type { get; set; }
            public string sms_optin_keyword { get; set; }
            public int messages_limit { get; set; }
            public int messages_sent { get; set; }            
        }

        public class ContactResponse
        {
            public string msisdn { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string status { get; set; }
        }

        public class ActionMessageResponse
        {
            public string result { get; set; }
        }

        public class GroupResponse
        {
            public string short_name { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public GroupMembers members { get; set; }
        }

        public class GroupMembers
        {
            public string total { get; set; }
            public string pending { get; set; }
            public string confirmed { get; set; }
        }

        public class MessageResponse
        {
            public int id { get; set; }
            public int recepients_count { get; set; }
            public string message { get; set; }
            public List<MessageRecipients> recipients { get; set; }
        }

        public class MessageRecipients
        {
            public string msisdn { get; set; }
        }

        public class InboxMessageResponse
        {
            public string status { get; set; }
            public string msisdn { get; set; }
            public string datetime { get; set; }
            public string message { get; set; }
            public int message_id { get; set; }
            public string short_number { get; set; }
        }

        public class MessageToGroupResponse
        {
            public int sms_sent { get; set; }
            public string sms_message { get; set; }
        }

        public class ScheduleMessageResponse
        {
            public int id { get; set; }
            public string name { get; set; }
            public string frequency { get; set; }
            public string message { get; set; }
            public DateTime date_expires { get; set; }
            public List<MessageGroup> groups { get; set; }
        }

        public class MessageGroup
        {
            public string short_name{ get; set; }
        }
        
    }
}
