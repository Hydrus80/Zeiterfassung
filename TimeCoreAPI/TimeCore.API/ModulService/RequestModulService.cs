using Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;
using TimeCore.ModulService;

namespace TimeCore.API
{
    public class RequestModulService : IRequestModulService
    {
        //Felder
        public ITimeCoreModulService timeCoreModulService;

        public ITimeCoreModulService GetCurrentTimeCoreSQLModulService()
        {
            if (timeCoreModulService is null)
                timeCoreModulService = new TimeCoreSQLModulService();
            return timeCoreModulService;
        }

        public RequestModel GetAuthenticatedUser(ClaimsPrincipal contextUser, RequestModel selectedRequest)
        {
            //INit
            if(selectedRequest is null)
                selectedRequest = new RequestModel();

            //Name und GUID holen
            if ((contextUser is ClaimsPrincipal) && (contextUser.Identity.IsAuthenticated))
            {
                selectedRequest.requestUserName = contextUser.Identity?.Name;
                selectedRequest.requestGUID = contextUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            //zurück
            return selectedRequest;
        }

        public async Task<string> AuthenticateAsync(RequestModel selectedRequest)
        {
            try
            {
                if ((string.IsNullOrEmpty(selectedRequest.requestUserName)) && (string.IsNullOrEmpty(selectedRequest.requestPassword)))
                    return string.Empty;
                string foundResult = await GetCurrentTimeCoreSQLModulService().LoginAsync(selectedRequest.requestUserName, selectedRequest.requestPassword).ConfigureAwait(false);
                if (!string.IsNullOrEmpty(foundResult))
                    return foundResult;
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
                    return foundResult;
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.Authenticate(): {ex.Message}");
                return string.Empty;
            }
        }

        public AccountModel GetAccountByGUID(RequestModel selectedRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedRequest.requestGUID))
                    return new AccountModel();
                return GetCurrentTimeCoreSQLModulService().Login(selectedRequest.requestGUID);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.GetAccountByGUID(): {ex.Message}");
                return new AccountModel();
            }
        }

        public async Task<AccountModel> GetAccountByGUIDAsync(RequestModel selectedRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedRequest.requestGUID))
                    return new AccountModel();
                return await GetCurrentTimeCoreSQLModulService().LoginAsync(selectedRequest.requestGUID).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.GetAccountByGUID(): {ex.Message}");
                return new AccountModel();
            }
        }

        public List<TimeStampModel> GetStampTimesMonthList(RequestModel selectedRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedRequest.requestGUID))
                    return new List<TimeStampModel>();
                List<TimeStampModel> foundResult = GetCurrentTimeCoreSQLModulService().GetStampTimesMonthList(selectedRequest.requestGUID, selectedRequest.requestYear, selectedRequest.requestMonth);
                if (foundResult is List<TimeStampModel>)
                    return foundResult;
                else
                    return new List<TimeStampModel>();
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.GetStampTimesMonthList(): {ex.Message}");
                return new List<TimeStampModel>();
            }
        }

        public TimeStampModel StampIn(RequestModel selectedRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedRequest.requestGUID))
                    return new TimeStampModel();
                TimeStampModel foundResult = GetCurrentTimeCoreSQLModulService().StampIn(selectedRequest.requestGUID,
                    selectedRequest.requestYear, selectedRequest.requestMonth, selectedRequest.requestDay, selectedRequest.requestHour,
                    selectedRequest.requestMinute, selectedRequest.requestSecond);
                if (foundResult is TimeStampModel)
                    return foundResult;
                else
                    return new TimeStampModel();
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.StampIn(): {ex.Message}");
                return new TimeStampModel();
            }
        }

        public TimeStampModel StampOut(RequestModel selectedRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedRequest.requestGUID))
                    return new TimeStampModel();
                TimeStampModel foundResult = GetCurrentTimeCoreSQLModulService().StampOut(selectedRequest.requestGUID,
                    selectedRequest.requestYear, selectedRequest.requestMonth, selectedRequest.requestDay, selectedRequest.requestHour,
                    selectedRequest.requestMinute, selectedRequest.requestSecond);
                if (foundResult is TimeStampModel)
                    return foundResult;
                else
                    return new TimeStampModel();
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RequestModulService.StampOut(): {ex.Message}");
                return new TimeStampModel();
            }
        }

    }
}
