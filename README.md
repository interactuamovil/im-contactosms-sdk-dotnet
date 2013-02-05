InteractúaMóvil SDK DotNet
==========================

SDK DotNet para el API SMS de InteractúaMóvil.

Es necesario poseer un **API_KEY**, un **API_SECRET_KEY** y **API_URL**
para utilizar el API.

Ejemplo de creación de instancia del api:
       
    sdk = new SmsApi(key, secret, url);

Para hacer llamadas al API puede utilizarse `Contacts`, `Groups`, `Messages` y
`Account` en el objeto de api:

	ResponseObjects.AccountStatusResponse account = (ResponseObjects.AccountStatusResponse) sdk.Account.Status();
	ResponseObjects.MessageToGroupResponse response = (ResponseObjects.MessageToGroupResponse) sdk.Messages.SendToContact(msisdn, "Prueba de mensaje");

Descarga
--------
Para descargar el último binario (dll): [im-contactosms-sdk-dotnet-0.1.2.zip](https://www.dropbox.com/s/qx60cr30codrq06/im-contactosms-sdk-dotnet-0.1.2.zip)


