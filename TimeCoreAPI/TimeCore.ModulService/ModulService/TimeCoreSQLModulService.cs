using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCore.ModelService;
using TimeCore.ModelService.EFC.SQL;
using static TimeCore.ModelService.SupportedDatabaseType;

namespace TimeCore.ModulService
{
    public class TimeCoreSQLModulService : ITimeCoreModulService
    {
        public eDatabaseType modelDatabaseType;
        public ITimeCoreModelService timeCoreModelService;
        public ITimeStampModelService timeStampModelService;
        public IAccountModelService accountAccountModelService;

        public TimeCoreSQLModulService()
        {
            modelDatabaseType = eDatabaseType.SQL;
        }

        public ITimeCoreModelService GetCurrentTimeCoreModelService()
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                if (timeCoreModelService is null)
                    timeCoreModelService = new TimeCoreModelService(modelDatabaseType);
                                     
            }
            return timeCoreModelService;
        }

        public List<TimeStampModel> GetStampTimesMonthList(string userGUID, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeCoreModelService().GetStampTimesMonthList(userGUID, selectedYear, selectedMonth);
            }
            else
                return null;
        }

        public async Task<List<TimeStampModel>> GetStampTimesMonthListAsync(string userGUID, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeCoreModelService().GetStampTimesMonthListAsync(userGUID, selectedYear, selectedMonth).ConfigureAwait(false);
            }
            else
                return null;
        }

        public string Login(string accountUserName, string accountPassword)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                AccountModel foundAccount = GetCurrentTimeCoreModelService().Authenticate(accountUserName, accountPassword);
                return foundAccount?.GUID;
            }
            else
                return null;
        }

        public async Task<string> LoginAsync(string accountUserName, string accountPassword)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                AccountModel foundAccount = await GetCurrentTimeCoreModelService().AuthenticateAsync(accountUserName, accountPassword).ConfigureAwait(false);
                return foundAccount?.GUID;
            }
            else
                return null;
        }

        public TimeStampModel StampIn(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeCoreModelService().StampIn(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampInAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeCoreModelService().StampInAsync(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).ConfigureAwait(false);
            }
            else
                return null;
        }

        public TimeStampModel StampOut(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeCoreModelService().StampOut(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampOutAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {

            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeCoreModelService().StampOutAsync(userGUID, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).ConfigureAwait(false);
            }
            else
                return null;
        }
    }
}
