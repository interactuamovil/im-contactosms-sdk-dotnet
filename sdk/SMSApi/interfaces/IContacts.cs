using System;
using System.Collections.Generic;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api.interfaces
{
    public interface IContacts
    {
        ResponseObjects.ApiResponse<List<ResponseObjects.ContactJson>> GetList(List<ContactStatus> contactStatuses = null, String query = null, int start = -1, int limit = -1, bool shortResults = false);
        ResponseObjects.ApiResponse<ResponseObjects.ContactJson> GetByMsisdn(string msisdn);
        ResponseObjects.ApiResponse<ResponseObjects.ContactJson> Update(string countryCode, string msisdn, string firstName = null, string lastName = null);
        ResponseObjects.ApiResponse<ResponseObjects.ContactJson> Add(string countryCode, string msisdn, string firstName = null, string lastName = null);
        ResponseObjects.ApiResponse<ResponseObjects.ContactJson> Delete(string msisdn);
        ResponseObjects.ApiResponse<List<ResponseObjects.GroupResponse>> GetGroupList(string msisdn);
    }
}
