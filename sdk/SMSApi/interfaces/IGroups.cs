using System;
using System.Collections.Generic;
using System.Text;

namespace InteractuaMovil.ContactoSms.Api.interfaces
{
    public interface IGroups
    {
        ResponseObjects.ApiResponse<List<ResponseObjects.GroupResponse>> GetList();
        ResponseObjects.ApiResponse<ResponseObjects.GroupResponse> Get(string ShortName);
        ResponseObjects.ApiResponse<ResponseObjects.ActionMessageResponse> Delete(string ShortName);
        ResponseObjects.ApiResponse<List<ResponseObjects.ContactJson>> GetContactList(string ShortName);
    }
}
