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

Documentación
-------------

La documentación del SDK DotNet se encontra en el wiki: [Ver documentación](https://github.com/interactuamovil/im-contactosms-sdk-dotnet/wiki)