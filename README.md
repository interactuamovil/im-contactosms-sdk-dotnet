InteractúaMóvil SDK DotNet
==========================

SDK DotNet para el API SMS de InteractúaMóvil.

Es necesario poseer un **API_KEY**, un **API_SECRET_KEY** y **API_URL**
para utilizar el API.

Ejemplo de creación de instancia del api:
       
    sdk = new SmsApi(key, secret, url);

Para hacer llamadas al API puede utilizarse `Contacts`, `Groups`, `Messages` y
`Account` en el objeto de api:

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
            ResponseObjects.ApiResponse<ResponseObjects.MessageToGroupResponse> response = sdk.Messages.SendToContact(msisdn, "Prueba de mensaje");
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

Para ver el ejemplo completo visita [ApiExample/Program.cs](https://github.com/interactuamovil/im-contactosms-sdk-dotnet/blob/master/sdk/ApiExample/Program.cs)

Descarga
--------
Para descargar el último binario (dll): [im-contactosms-sdk-dotnet-0.1.2.zip](https://www.dropbox.com/s/qx60cr30codrq06/im-contactosms-sdk-dotnet-0.1.2.zip)


