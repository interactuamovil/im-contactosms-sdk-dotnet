using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api
{
    public class SmsApi
    {
        public interfaces.IContacts Contacts;
        public interfaces.IGroups Groups;
        public interfaces.IMessages Messages;
        public interfaces.IAccounts Account;

        public SmsApi(string ApiKey, string SecretKey, string ApiUrl)
        {
            this.Account = new Accounts(ApiKey, SecretKey, ApiUrl);
            this.Contacts = new Contacts(ApiKey, SecretKey, ApiUrl);
            this.Groups = new Groups(ApiKey, SecretKey, ApiUrl);
            this.Messages = new Messages(ApiKey, SecretKey, ApiUrl);
        }

        public SmsApi(string ApiKey, string SecretKey, string ApiUrl, string ProxyAddress, string UserName, string Password)
        {
            this.Account = new Accounts(ApiKey, SecretKey, ApiUrl, ProxyAddress, UserName, Password);
            this.Contacts = new Contacts(ApiKey, SecretKey, ApiUrl, ProxyAddress, UserName, Password);
            this.Groups = new Groups(ApiKey, SecretKey, ApiUrl, ProxyAddress, UserName, Password);
            this.Messages = new Messages(ApiKey, SecretKey, ApiUrl, ProxyAddress, UserName, Password);        
        }

    }
}
