using System;
using System.Collections.Generic;
using System.Text;
using InteractuaMovil.ContactoSms.Api;

namespace ApiExample
{

    class Program
    {

        static string msisdn = "50252017507";
        static InteractuaMovil.ContactoSms.Api.SmsApi sdk;

        static void Main(string[] args)
        {
            // API Key
            string key = "1d4e705080edec039fe580dd26fd1927";

            // API Secret
            string secret = "0b9aa43039efacc16072a9774af72993";

            // API Url
            string url = "https://smscorporativo.tigo.com.gt/api/rest/";

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
            Console.WriteLine("HTTP Response [{0}]: {1}", (int)account.HttpCode, account.HttpDescription);
            Console.WriteLine("JSON: {0}", account.Response);
            Console.WriteLine("----");

            if (account.isOk)
            {
                Console.WriteLine("Nombre de la cuenta: {0}", account.Data.name);
                Console.WriteLine("Nombre corto sms: {0}", account.Data.sms_short_name);
                Console.WriteLine("Tipo de suscripcion sms: {0}", account.Data.sms_subscription_type);
                Console.WriteLine("Keyword de optin: {0}", account.Data.sms_optin_keyword);
                Console.WriteLine("Limite de mensajes: {0}", account.Data.messages_limit);
                Console.WriteLine("Mensajes enviados: {0}", account.Data.messages_sent);
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", account.ErrorCode, account.ErrorDescription);
            }

        }                

        private static void SendSingleContactoMessage()
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Envio Mensaje");
            ResponseObjects.ApiResponse<ResponseObjects.MessageResponse> response = sdk.Messages.SendToContact(msisdn, "Prueba de mensaje", "101");
            Console.WriteLine("HTTP Response [{0}]: {1}", (int)response.HttpCode, response.HttpDescription);
            Console.WriteLine("JSON: {0}", response.Response);
            Console.WriteLine("----");

            if (response.isOk)
            {
                Console.WriteLine("Enviado: {0}", response.Data.SentCount);
                Console.WriteLine("Mensaje: {0}", response.Data.Message);
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", response.ErrorCode, response.ErrorDescription);
            }

        }

        private static void GetMessageLog()
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Listado de Mensajes");

            ResponseObjects.ApiResponse<List<ResponseObjects.MessageResponse>> response = sdk.Messages.GetList(StartDate: DateTime.Today.AddDays(-17), IncludeRecipients: true);
            Console.WriteLine("HTTP Response [{0}]: {1}", (int)response.HttpCode, response.HttpDescription);
            Console.WriteLine("JSON: {0}", response.Response);
            Console.WriteLine("----");

            if (response.isOk)
            {
                /*Console.WriteLine("Enviado: {0}", response.data.sms_sent);
                Console.WriteLine("Mensaje: {0}", response.data.sms_message);*/
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", response.ErrorCode, response.ErrorDescription);
            }

        }

    }

}
