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

            //AccountStatus();  
            
            SendSingleContactoMessage();

            GetMessageLog();

            Console.Read();

        }        

        private static void AccountStatus()
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Consulta Status");

            ResponseObjects.ApiResponse<ResponseObjects.AccountStatusResponse> account = sdk.Account.Status();
            Console.WriteLine("HTTP Response [{0}]: {1}", (int)account.httpCode, account.httpDescription);
            Console.WriteLine("JSON: {0}", account.response);
            Console.WriteLine("----");

            if (account.isOk)
            {
                Console.WriteLine("Nombre de la cuenta: {0}", account.data.name);
                Console.WriteLine("Nombre corto sms: {0}", account.data.sms_short_name);
                Console.WriteLine("Tipo de suscripcion sms: {0}", account.data.sms_subscription_type);
                Console.WriteLine("Keyword de optin: {0}", account.data.sms_optin_keyword);
                Console.WriteLine("Limite de mensajes: {0}", account.data.messages_limit);
                Console.WriteLine("Mensajes enviados: {0}", account.data.messages_sent);
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", account.errorCode, account.errorDescription);
            }

        }                

        private static void SendSingleContactoMessage()
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Envio Mensaje");
            ResponseObjects.ApiResponse<ResponseObjects.MessageToGroupResponse> response = sdk.Messages.SendToContact(msisdn, "Prueba de mensaje", "100");
            Console.WriteLine("HTTP Response [{0}]: {1}", (int)response.httpCode, response.httpDescription);
            Console.WriteLine("JSON: {0}", response.response);
            Console.WriteLine("----");

            if (response.isOk)
            {
                Console.WriteLine("Enviado: {0}", response.data.sms_sent);
                Console.WriteLine("Mensaje: {0}", response.data.sms_message);
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", response.errorCode, response.errorDescription);
            }

        }

        private static void GetMessageLog()
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Listado de Mensajes");

            ResponseObjects.ApiResponse<List<ResponseObjects.MessageResponse>> response = sdk.Messages.GetList(StartDate: DateTime.Today.AddDays(-17), IncludeRecipients: true);
            Console.WriteLine("HTTP Response [{0}]: {1}", (int)response.httpCode, response.httpDescription);
            Console.WriteLine("JSON: {0}", response.response);
            Console.WriteLine("----");

            if (response.isOk)
            {
                /*Console.WriteLine("Enviado: {0}", response.data.sms_sent);
                Console.WriteLine("Mensaje: {0}", response.data.sms_message);*/
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", response.errorCode, response.errorDescription);
            }

        }

    }

}
