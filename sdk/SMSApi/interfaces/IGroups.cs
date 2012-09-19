using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api.interfaces
{
    public interface IGroups
    {
        object GetList();
        object Get(string ShortName);
        object Update(string ShortName, string Name, string description, string NewShortName = null);
        object Add(string ShortName, string Name, string Description);
        object Delete(string ShortName);
        object GetContactList(string ShortName);
        object AddContact(string ShortName, string Msisdn);

    }
}
