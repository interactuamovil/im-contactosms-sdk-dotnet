using System;
using System.Collections.Generic;
using System.Text;
using InteractuaMovil.ContactoSms.Api;

namespace ApiExample
{

    class Program
    {

        
        static InteractuaMovil.ContactoSms.Api.SmsApi sdk;

        static void Main(string[] args)
        {
            // API Key
            string key = "";

            // API Secret
            string secret = "";

            // API Url
            string url = ""; /* ej: http://<url>/api/ */


            sdk = new SmsApi(key, secret, url);

            string groupName = "";
            string msisdn = "";
            string firstname = "";
            String lastname = "";


            /****** Test for messages ******/
            //SendMessageToContact(msisdn);
            //SendMessageToGroup(groupName);

            //GetMessageLog();


            /***** Test procedures for contacts ******/
            //CreateNewContact(msisdn, firstname, lastname);
            //GetContactByMsisdn(msisdn);

            /****** Test procedures for groups *******/
            //GetGroupList();
            //GetGroup(groupName);
            //GetContactListbyGroup(groupName);
            DeleteGroup(groupName);
            
            
            

            
            Console.Read();

        }

        private static void GetGroupList()
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Consulta Lista de Grupos");

            ResponseObjects.ApiResponse<List<InteractuaMovil.ContactoSms.Api.ResponseObjects.GroupResponse>> groups = sdk.Groups.GetList();
            if (groups.isOk)
            {
                foreach (InteractuaMovil.ContactoSms.Api.ResponseObjects.GroupResponse group in groups.Data) {
                    Console.WriteLine("Group name: " + group.name);
                }

                
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", groups.ErrorCode, groups.ErrorDescription);
            }
        }

        private static void GetGroup(string groupName)
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Consulta Grupo");

            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.GroupResponse> group = sdk.Groups.Get(groupName);
            if (group.isOk)
            {
                 Console.WriteLine("Group name: " + group.Data.name);
                 Console.WriteLine("Group short name: " + group.Data.short_name);

            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", group.ErrorCode, group.ErrorDescription);
            }
        }

        private static void GetContactListbyGroup(string groupName)
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Get Contactos por Grupo");

            ResponseObjects.ApiResponse<List<InteractuaMovil.ContactoSms.Api.ResponseObjects.ContactJson>> contacts = sdk.Groups.GetContactList(groupName);
            if (contacts.isOk)
            {
                foreach (InteractuaMovil.ContactoSms.Api.ResponseObjects.ContactJson contact in contacts.Data)
                {
                    Console.WriteLine("Contact name: " + contact.FirstName + " " + contact.LastName);
                    Console.WriteLine("Phone number: " + contact.PhoneNumber);
                }


            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", contacts.ErrorCode, contacts.ErrorDescription);
            }
        }

        private static void DeleteGroup(string groupName)
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Eliminar Grupos");

            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ActionMessageResponse> group = sdk.Groups.Delete(groupName);
            if (group.isOk)
            {
                Console.WriteLine("Group deleted");

            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", group.ErrorCode, group.ErrorDescription);
            }
        }
        /*
         * Esta opcion ya no esta disponible
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

        }       */         

        public static void CreateNewContact(string msisdn, string firstname, string lastname )
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Crear Contacto");
            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ContactJson> Contact = sdk.Contacts.Add("502", msisdn, firstname, lastname);
            if (Contact.isOk)
            {
                Console.WriteLine(Contact.Data.Msisdn + " - " + Contact.Data.FirstName + " " + Contact.Data.LastName);
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", Contact.ErrorCode, Contact.ErrorDescription);
            }
            
            Console.WriteLine("----");


        }

        public static void GetContactByMsisdn(string msisdn)
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Listar Contacto");
            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ContactJson> Contact = sdk.Contacts.GetByMsisdn(msisdn);
            if (Contact.isOk)
            {
                Console.WriteLine(Contact.Data.FirstName + " " + Contact.Data.LastName);
            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", Contact.ErrorCode, Contact.ErrorDescription);
            }
            Console.WriteLine("----");
        }

        private static void SendMessageToContact(String msisdn)
        {
            Random rnd = new Random();

            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Envio Mensaje a Contacto");
            ResponseObjects.ApiResponse<ResponseObjects.MessageResponse> response = sdk.Messages.SendToContact(msisdn, "Hola!", rnd.Next().ToString());
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

        private static void SendMessageToGroup(String groupName)
        {
            Random rnd = new Random();

            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Envio Mensaje a Grupo");
            ResponseObjects.ApiResponse<ResponseObjects.MessageResponse> response = sdk.Messages.SendToGroups(new String[] {groupName}, "Mensaje a grupo", rnd.Next().ToString());
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

            ResponseObjects.ApiResponse<List<ResponseObjects.MessageResponse>> response = sdk.Messages.GetList(StartDate: DateTime.Today.AddDays(-5), Direction: MessageDirection.MT);
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
