using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TimeCore.API
{
    public interface IRequestModulService
    {
        RequestModel GetAuthenticatedUser(ClaimsPrincipal contextUser, RequestModel selectedRequest);
        Task<string> AuthenticateAsync(RequestModel selectedRequest);
        string Authenticate(RequestModel selectedRequest);
        AccountModel GetAccountByGUID(RequestModel selectedRequest);
        Task<AccountModel> GetAccountByGUIDAsync(RequestModel selectedRequest);
        List<TimeStampModel> GetStampTimesList(RequestModel selectedRequest);
        TimeStampModel StampIn(RequestModel selectedRequest);
        TimeStampModel StampOut(RequestModel selectedRequest);
    }
}
