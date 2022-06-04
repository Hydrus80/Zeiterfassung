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

        public TimeCoreSQLModulService(ITimeStampModelService selectedTimeStampModelService, IAccountModelService selectedAccountAccountModelService)
        {
            modelDatabaseType = eDatabaseType.SQL;
            timeStampModelService = selectedTimeStampModelService;
            accountAccountModelService = selectedAccountAccountModelService;
        }

        public ITimeCoreModelService GetCurrentTimeCoreModelService()
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                if (timeCoreModelService is null)
                    timeCoreModelService = new TimeCoreModelService(modelDatabaseType, timeStampModelService, accountAccountModelService);
                                     
            }
            return timeCoreModelService;
        }

        public List<TimeStampModel> GetStampTimesMonthList(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeCoreModelService().GetStampTimesMonthList(userAccount, selectedYear, selectedMonth);
            }
            else
                return null;
        }

        public async Task<List<TimeStampModel>> GetStampTimesMonthList_Async(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeCoreModelService().GetStampTimesMonthList_Async(userAccount, selectedYear, selectedMonth).ConfigureAwait(false);
            }
            else
                return null;
        }

        public AccountModel Login(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeCoreModelService().Login(accountUserName, accountPassword, workshopID);
            }
            else
                return null;
        }

        public async Task<AccountModel> Login_Async(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeCoreModelService().Login_Async(accountUserName, accountPassword, workshopID).ConfigureAwait(false);
            }
            else
                return null;
        }

        public TimeStampModel StampIn(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeCoreModelService().StampIn(userAccount, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampIn_Async(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeCoreModelService().StampIn_Async(userAccount, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).ConfigureAwait(false);
            }
            else
                return null;
        }

        public TimeStampModel StampOut(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeCoreModelService().StampOut(userAccount, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond);
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampOut_Async(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {

            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeCoreModelService().StampOut_Async(userAccount, timeStampYear, timeStampMonth, timeStampDay, timeStampHour, timeStampMinute, timeStampSecond).ConfigureAwait(false);
            }
            else
                return null;
        }
    }
}
