using System;

namespace InteractuaMovil.ContactoSms.Api
{
	public class SmsApiTest
	{
		public SmsApiTest ()
		{			
			SmsApi api = new SmsApi("", "", "");
			object list = api.Contacts.GetList();
			Console.Write(list.ToString());
		}
	}
}