using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCore.API
{
    public interface IRequestModulService
    {
        Task<string> AuthenticateAsync(RequestModel selectedRequest);
        string Authenticate(RequestModel selectedRequest);
        RequestModel CheckGUIDLoggedIn(string selectedGUID);
    }
}
