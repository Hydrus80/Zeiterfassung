using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;
using TimeCore.ModulService;

namespace TimeCore.API
{
    public class RequestModulService : IRequestModulService
    {
        //Felder
        public ITimeCoreModulService timeCoreModulService;
        public static ConcurrentDictionary<string, RequestModel> requestModelList = new ConcurrentDictionary<string, RequestModel>();

        public ITimeCoreModulService GetCurrentTimeCoreSQLModulService()
        {
            if (timeCoreModulService is null)
                timeCoreModulService = new TimeCoreSQLModulService();
            return timeCoreModulService;
        }

        public async Task<string> AuthenticateAsync(RequestModel selectedRequest)
        {
            try
            {
                if ((string.IsNullOrEmpty(selectedRequest.requestUserName)) && (string.IsNullOrEmpty(selectedRequest.requestPassword)))
                    return string.Empty;
                string foundResult = await GetCurrentTimeCoreSQLModulService().LoginAsync(selectedRequest.requestUserName, selectedRequest.requestPassword).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(foundResult))
                {
                    selectedRequest.requestGUID = foundResult;
                    requestModelList.AddOrUpdate(
                          foundResult,
                          selectedRequest,
                          (key, oldValue) => selectedRequest);
                    return foundResult;
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.AuthenticateAsync(): {ex.Message}");
                return string.Empty;
            }
        }

        public string Authenticate(RequestModel selectedRequest)
        {
            try
            {
                if ((string.IsNullOrEmpty(selectedRequest.requestUserName)) && (string.IsNullOrEmpty(selectedRequest.requestPassword)))
                    return string.Empty;
                string foundResult = GetCurrentTimeCoreSQLModulService().Login(selectedRequest.requestUserName, selectedRequest.requestPassword);
                if (!string.IsNullOrEmpty(foundResult))
                {
                    selectedRequest.requestGUID = foundResult;
                    requestModelList.AddOrUpdate(
                          foundResult,
                          selectedRequest,
                          (key, oldValue) => selectedRequest);
                    return foundResult;
                }
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.Authenticate(): {ex.Message}");
                return string.Empty;
            }
        }

        public RequestModel CheckGUIDLoggedIn(string selectedGUID)
        {
            //Init
            RequestModel returnRequest = null;

            try
            {
                if (string.IsNullOrEmpty(selectedGUID))
                    return returnRequest;
                if (requestModelList.ContainsKey(selectedGUID))
                {
                    returnRequest = requestModelList[selectedGUID];
                }
                return returnRequest;
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.CheckGUIDLoggedIn(): {ex.Message}");
                return returnRequest;
            }
        }
    }
}
