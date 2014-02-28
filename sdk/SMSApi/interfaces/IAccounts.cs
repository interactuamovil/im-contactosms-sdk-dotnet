using System;
using System.Collections.Generic;
using System.Text;
using InteractuaMovil.ContactoSms.Api;

namespace InteractuaMovil.ContactoSms.Api.interfaces
{
    public interface IAccounts
    {
        ResponseObjects.ApiResponse<ResponseObjects.AccountStatusResponse> Status();
    }
}
