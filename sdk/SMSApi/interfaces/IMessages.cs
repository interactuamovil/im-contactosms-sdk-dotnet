using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api.interfaces
{
    public interface IMessages
    {
        object GetList(DateTime? StartDate = null, DateTime? EndDate = null, int Start=-1, int Limit=-1, string Msisdn=null);
        object SendToGroups (string[] ShortName, string Message);
        object SendToContact (string Msisdn, string Message) ;
        object GetSchedule () ;
        object RemoveSchedule (string MessageId) ;
        object AddSchedule (DateTime StartDate, DateTime EndDate, string Name, string Message, string Time, string Frequency, string[] Groups);
        object Inbox (DateTime? StartDate=null, DateTime? EndDate=null, int Start=-1, int Limit=-1, string Msisdn=null,  int Status=-1);
    }
}
