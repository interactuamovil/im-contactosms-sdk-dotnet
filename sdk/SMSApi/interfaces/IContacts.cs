using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api.interfaces
{
    public interface IContacts
    {
        ResponseObjects.ApiResponse<List<ResponseObjects.ContactResponse>> GetList(int Start = -1, int Limit = -1, string FirstName = null, string LastName = null, int Status = -1);
        ResponseObjects.ApiResponse<ResponseObjects.ContactResponse> GetByMsisdn(string Msisdn);
        ResponseObjects.ApiResponse<ResponseObjects.ActionMessageResponse> Update(string Msisdn, string FirstName = null, string LastName = null, string NewMsisdn = null);
        ResponseObjects.ApiResponse<ResponseObjects.ActionMessageResponse> Add(string Msisdn, string FirstName, string LastName);
        ResponseObjects.ApiResponse<ResponseObjects.ActionMessageResponse> Delete(string Msisdn);
        ResponseObjects.ApiResponse<List<ResponseObjects.GroupResponse>> GetGroupList(string Msisdn);
    }
}
