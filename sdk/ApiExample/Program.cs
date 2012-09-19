using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApiExample
{

    class Program
    {

        static string msisdn = "50230215255";
        static InteractuaMovil.ContactoSms.Api.SmsApi sdk;

        static void Main(string[] args)
        {
            // API Key
            string key = "";

            // API Secret
            string secret = "";

            // API Url
            string url = "https://apps.interactuamovil.com/tigocorp/api/";

            sdk = new InteractuaMovil.ContactoSms.Api.SmsApi(key, secret, url);

            AccountStatus();
            //SendSingleContactoMessage();

            Console.Read();

        }        

        private static void AccountStatus()
        {

            InteractuaMovil.ContactoSms.Api.ResponseObjects.AccountStatusResponse account = (InteractuaMovil.ContactoSms.Api.ResponseObjects.AccountStatusResponse)sdk.Account.Status();
            Console.WriteLine("Limite de mensajes: {0}", account.message_limit);

        }

        private static void SendSingleContactoMessage()
        {

            InteractuaMovil.ContactoSms.Api.ResponseObjects.MessageToGroupResponse response = (InteractuaMovil.ContactoSms.Api.ResponseObjects.MessageToGroupResponse)sdk.Messages.SendToContact(msisdn, "Prueba de mensaje");            
            Console.WriteLine("Enviados: {0}", response.sms_count);

        }

    }

}
