using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api.interfaces
{
    public interface IGroups
    {
        ResponseObjects.ApiResponse<List<ResponseObjects.GroupResponse>> GetList();
        ResponseObjects.ApiResponse<ResponseObjects.GroupResponse> Get(string ShortName);
        ResponseObjects.ApiResponse<ResponseObjects.ActionMessageResponse> Update(string ShortName, string Name, string Description, string NewShortName = null);
        ResponseObjects.ApiResponse<ResponseObjects.ActionMessageResponse> Add(string ShortName, string Name, string Description);
        ResponseObjects.ApiResponse<ResponseObjects.ActionMessageResponse> Delete(string ShortName);
        ResponseObjects.ApiResponse<List<ResponseObjects.ContactResponse>> GetContactList(string ShortName);
        ResponseObjects.ApiResponse<ResponseObjects.ActionMessageResponse> AddContact(string ShortName, string Msisdn);
    }
}
