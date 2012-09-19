using System;

namespace InteractuaMovil.ContactoSms.Api
{
	public class SmsApiTest
	{
		public SmsApiTest ()
		{			
			SmsApi api = new SmsApi("http://im.dev/srsms/api", "070939561773990de26d3cb0e7290275", "4b1534e2f29ce85cdd2b4e49cfa3f8a8");
			object list = api.Contacts.GetList();
			Console.Write(list.ToString());
		}
	}
}