using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api.interfaces
{
    public interface IContacts
    {
        object GetList(int Start = -1, int Limit = -1, string FirstName = null, string LastName = null, int Status = -1);
        object GetByMsisdn(string Msisdn);
        object Update(string Msisdn, string FirstName = null, string LastName = null, int Status = -1, string NewMsisdn = null);
        object Add(string Msisdn, string FirstName, string LastName, int Status);
        object Delete(string Msisdn);
        object GetGroupList(string Msisdn);
              
    }
}
