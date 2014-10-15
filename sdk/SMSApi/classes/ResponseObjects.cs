using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InteractuaMovil.ContactoSms.Api
{
    public class ResponseObjects
    {
        public class ApiResponse<T>
        {
            public String Response { get; set; }
            public T Data { get; set; }
            public System.Net.HttpStatusCode HttpCode { get; set; }
            public String HttpDescription { get; set; }
            public Int32 ErrorCode { get; set; }
            public String ErrorDescription { get; set; }

            public ApiResponse()
            {
                Response = String.Empty;
                Data = default(T);
                ErrorCode = Convert.ToInt32(System.Net.HttpStatusCode.OK);
                ErrorDescription = System.Net.HttpStatusCode.OK.ToString();
            }

            public Boolean isOk
            {
                get { return (HttpCode == System.Net.HttpStatusCode.OK); }
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

        public class ContactJson
        {
            [JsonPropertyAttribute("msisdn")]
            public String Msisdn;
            [JsonPropertyAttribute("phone_number")]
            public String PhoneNumber;
            [JsonPropertyAttribute("country_code")]
            public String CountryCode;
            [JsonPropertyAttribute("first_name")]
            public String FirstName;
            [JsonPropertyAttribute("last_name")]
            public String LastName;
            [JsonPropertyAttribute("full_name")]
            public String FullName;
            [JsonPropertyAttribute("email")]
            public String Email;
            [JsonPropertyAttribute("status")]
            [JsonConverter(typeof(StringEnumConverter))]
            public ContactStatus Status;
            [JsonPropertyAttribute("added_from")]
            [JsonConverter(typeof(StringEnumConverter))]
            public AddedFrom AddedFrom;
    
            [JsonPropertyAttribute("custom_field_1")]
            public String CustomField1;
            [JsonPropertyAttribute("custom_field_2")]
            public String CustomField2;
            [JsonPropertyAttribute("custom_field_3")]
            public String CustomField3;
            [JsonPropertyAttribute("custom_field_4")]
            public String CustomField4;
            [JsonPropertyAttribute("custom_field_5")]
            public String CustomField5;
    
            [JsonPropertyAttribute("tags")]
            public List<String> Tags;

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
            [JsonPropertyAttribute("message_id")]
            public Int32 MessageId { get; set; }
            [JsonPropertyAttribute("short_code")]
            public String ShortCode;
            [JsonPropertyAttribute("type")]
            public Int32 MessageTypeId;
            [JsonPropertyAttribute("direction")]
            [JsonConverter(typeof(StringEnumConverter))]
            public MessageDirection MessageDirection;
            [JsonPropertyAttribute("status")]
            [JsonConverter(typeof(StringEnumConverter))]
            public MessageStatus MessageStatus;
            [JsonPropertyAttribute("sent_from")]
            [JsonConverter(typeof(StringEnumConverter))]
            public MessageSentFrom SentFrom;
            [JsonPropertyAttribute("id")]
            public String ClientMessageId;
            [JsonPropertyAttribute("message") ]
            public String Message;
            [JsonPropertyAttribute("sent_count")]
            public Int32 SentCount;
            [JsonPropertyAttribute("error_count")]
            public Int32 ErrorCount;
            [JsonPropertyAttribute("total_recipients")]
            public Int32 TotalRecipients;
            [JsonPropertyAttribute("msisdn")]
            public String Msisdn;
            [JsonPropertyAttribute("country")]
            public String CountryCode;
            [JsonPropertyAttribute("is_billable")]
            public bool Billable;
            [JsonPropertyAttribute("is_scheduled")]
            public bool Scheduled;
            [JsonPropertyAttribute("created_on")]
            [JsonConverter(typeof(JsonDateTimeConverter))]
            public DateTime CreatedOn;
    
            [JsonPropertyAttribute("created_by")]
            public String CreatedBy;
    
            [JsonPropertyAttribute("total_monitors")]
            public Int32 TotalMonitors;
    
            [JsonPropertyAttribute("groups")]
            public List<String> Groups;
    
            [JsonPropertyAttribute("recipients")]
            public List<RecipientJson> Recipients;
        
            /* LEGACY v2 */
            [Obsolete("smsSent is deprecated, please use sentCount instead.", true)]
            [JsonPropertyAttribute("sms_sent")]
            public Int32 smsSent;
            [Obsolete("smsMessage is deprecated, please use message instead", true)]
            [JsonPropertyAttribute("sms_message")]
            public String smsMessage;
        }

        public class RecipientJson
        {
            [JsonPropertyAttribute("msisdn")]
            private String Msisdn;
            [JsonPropertyAttribute("country")]
            private String CountryCode;
            [JsonPropertyAttribute("status")]
            [JsonConverter(typeof(StringEnumConverter))]
            private MessageStatus MessageStatus;
        }

        public class ScheduledMessageJson
        {
            [JsonPropertyAttribute("scheduled_message_id")]
            public Int32 ScheduledMessageId;
            [JsonPropertyAttribute("groups")] 
            public List<String> Groups;
            [JsonPropertyAttribute("message")] 
            public String Message;
            [JsonPropertyAttribute("id")]
            public String ClientMessageId;
            [JsonPropertyAttribute("event_name")]
            public String EventName;
            [JsonPropertyAttribute("start_date")]
            public DateTime StartDate;
            [JsonPropertyAttribute("end_date")]
            public DateTime EndDate;
            [JsonPropertyAttribute("execution_time")]
            public DateTime ExecutionTime;
            [JsonPropertyAttribute("repeat_interval")]
            [JsonConverter(typeof(StringEnumConverter))]
            public RepeatInterval RepeatInterval;
            [JsonPropertyAttribute("repeat_days")]
            public String RepeatDays;
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
