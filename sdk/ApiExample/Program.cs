using System;
using System.Collections.Generic;
using System.Text;
using InteractuaMovil.ContactoSms.Api;

namespace ApiExample
{

    class Program
    {

        static string msisdn = "50244721242";
        static InteractuaMovil.ContactoSms.Api.SmsApi sdk;

        static void Main(string[] args)
        {
            // API Key
            string key = "1d4e705080edec039fe580dd26fd1927";

            // API Secret
            string secret = "0b9aa43039efacc16072a9774af72993";

            // API Url
            string url = "http://apps2.im:8088/api/";

            sdk = new SmsApi(key, secret, url);

            string groupName = "Fijese";

            //AccountStatus();  
            //CreateNewContact();
            //GetContactByMsisdn();
            //SendSingleContactoMessage();
            //GetMessageLog();

            //AddGroup();
            //GetGroupList();
            //GetGroup(groupName);
            //GetContactListbyGroup(groupName);
            //DeleteGroup();
            //AddContactToGroup();
            //RemoveContactFromGroup(groupName, msisdn);
            //UpdateGroup();

            Console.Read();

        }

        private static void GetGroupList()
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Consulta Status");

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
            Console.WriteLine("Prueba Consulta Status");

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

        private static void DeleteGroup()
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Eliminar Grupos");

            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ActionMessageResponse> group = sdk.Groups.Delete("Fijese");
            if (group.isOk)
            {
                Console.WriteLine("Group deleted");

            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", group.ErrorCode, group.ErrorDescription);
            }
        }

        private static void AddGroup()
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Agregar Grupos");

            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ActionMessageResponse> group = sdk.Groups.Add("Fijese","Fijese","Test group");
            if (group.isOk)
            {
                Console.WriteLine("Group added");

            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", group.ErrorCode, group.ErrorDescription);
            }
        }

        private static void AddContactToGroup()
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Agregar Contacto a Grupos");

            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ActionMessageResponse> group = sdk.Groups.AddContact("Fijese", msisdn);
            if (group.isOk)
            {
                Console.WriteLine("Contact added to group");

            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", group.ErrorCode, group.ErrorDescription);
            }

        }

        private static void RemoveContactFromGroup(string groupName, string Msisdn)
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Eliminar Contacto de Grupo");

            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ActionMessageResponse> group = sdk.Groups.RemoveContact(groupName, Msisdn);
            if (group.isOk)
            {
                Console.WriteLine("Contact deleted from group");

            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", group.ErrorCode, group.ErrorDescription);
            }

        }

        private static void UpdateGroup()
        {
            Console.WriteLine("Demo API SDK .net");
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Modificar Grupos");

            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ActionMessageResponse> group = sdk.Groups.Update("Fijese","Fijese 1","Grupo de prueba");
            if (group.isOk)
            {
                Console.WriteLine("Group updated" + group.Data.result);

            }
            else
            {
                Console.WriteLine("Error[{0}]: {1}", group.ErrorCode, group.ErrorDescription);
            }
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
            ResponseObjects.ApiResponse<ResponseObjects.MessageResponse> response = sdk.Messages.SendToContact(msisdn, "Hola!", "116");
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

        public static void CreateNewContact()
        {
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("Prueba Crear Contacto");
            ResponseObjects.ApiResponse<InteractuaMovil.ContactoSms.Api.ResponseObjects.ContactJson> Contact = sdk.Contacts.Add("502","50230535379","Gerardo","Garcia");
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

        public static void GetContactByMsisdn()
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
