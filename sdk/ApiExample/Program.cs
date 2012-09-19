using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteractuaMovil.ContactoSms.Api;

namespace ApiExample
{

    class Program
    {

        static string msisdn = "";
        static InteractuaMovil.ContactoSms.Api.SmsApi sdk;

        static void Main(string[] args)
        {
            // API Key
            string key = "";

            // API Secret
            string secret = "";

            // API Url
            string url = "";

            sdk = new SmsApi(key, secret, url);

            AccountStatus();            
            SendSingleContactoMessage();

            Console.Read();

        }        

        private static void AccountStatus()
        {

            ResponseObjects.AccountStatusResponse account = (ResponseObjects.AccountStatusResponse) sdk.Account.Status();
            Console.WriteLine("Nombre de la cuenta: {0}", account.name);
            Console.WriteLine("Nombre corto sms: {0}", account.sms_short_name);
            Console.WriteLine("Tipo de suscripcion sms: {0}", account.sms_subscription_type);
            Console.WriteLine("Keyword de optin: {0}", account.sms_optin_keyword);
            Console.WriteLine("Limite de mensajes: {0}", account.messages_limit);
            Console.WriteLine("Mensajes enviados: {0}", account.messages_sent);

        }                

        private static void SendSingleContactoMessage()
        {

            ResponseObjects.MessageToGroupResponse response = (ResponseObjects.MessageToGroupResponse) sdk.Messages.SendToContact(msisdn, "Prueba de mensaje");            
            Console.WriteLine("Enviados: {0}", response.sms_sent);

        }

    }

}
