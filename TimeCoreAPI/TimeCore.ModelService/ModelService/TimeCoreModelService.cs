using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCore.ModelService.EFC.SQL;
using static TimeCore.ModelService.SupportedDatabaseType;

namespace TimeCore.ModelService
{
    public class TimeCoreModelService : ITimeCoreModelService
    {
        public eDatabaseType modelDatabaseType;
        public ITimeStampModelService timeStampModelService;
        public IAccountModelService accountModelService;

        public TimeCoreModelService(eDatabaseType selectedDatabaseType)
        {
            modelDatabaseType = selectedDatabaseType;
        }

        public ITimeStampModelService GetCurrentTimeStampModelService()
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                if (timeStampModelService is null)
                    timeStampModelService = new TimeStampModelService(modelDatabaseType, new TimeStampSQLRepository());
            }
            return timeStampModelService;
        }

        public IAccountModelService GetCurrentAccountModelService()
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                if (accountModelService is null)
                    accountModelService = new AccountModelService(modelDatabaseType, new AccountSQLRepository());
            }
            return accountModelService;
        }

        public List<TimeStampModel> GetStampTimesMonthList(string userGUID, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeStampModelService().GetTimeStampList(userGUID, selectedYear, selectedMonth);
            }
            else
                return null;
        }

        public async Task<List<TimeStampModel>> GetStampTimesMonthListAsync(string userGUID, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeStampModelService().GetTimeStampListAsync(userGUID, selectedYear, selectedMonth).ConfigureAwait(false);
            }
            else
                return null;
        }

        public AccountModel Authenticate(string accountUserName, string accountPassword)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentAccountModelService().Authenticate(accountUserName, accountPassword);
            }
            else
                return null;
        }

        public async Task<AccountModel> AuthenticateAsync(string accountUserName, string accountPassword)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentAccountModelService().AuthenticateAsync(accountUserName, accountPassword).ConfigureAwait(false);
            }
            else
                return null;
        }

        public AccountModel Authenticate(string accountGUID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentAccountModelService().GetAccountByGUID(accountGUID);
            }
            else
                return null;
        }

        public async Task<AccountModel> AuthenticateAsync(string accountGUID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentAccountModelService().GetAccountByGUIDAsync(accountGUID).ConfigureAwait(false);
            }
            else
                return null;
        }

        public TimeStampModel StampIn(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampIn = new TimeStampModel();
                //Acount holen
                AccountModel userAccount = GetCurrentAccountModelService().GetAccountByGUID(userGUID);
                if (userAccount?.ID > 0)
                {
                    stampIn.Account = userAccount;
                    stampIn.TimeStampYear = timeStampYear;
                    stampIn.TimeStampMonth = timeStampMonth;
                    stampIn.TimeStampDay = timeStampDay;
                    stampIn.TimeStampHour = timeStampHour;
                    stampIn.TimeStampMinute = timeStampMinute;
                    stampIn.TimeStampSecond = timeStampSecond;
                    stampIn.LastUpdate = DateTime.Now;
                    return GetCurrentTimeStampModelService().AddTimeStamp(stampIn);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampInAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampIn = new TimeStampModel();
                DateTime stampTime = DateTime.Now;
                //Acount holen
                AccountModel userAccount = GetCurrentAccountModelService().GetAccountByGUID(userGUID);
                if (userAccount?.ID > 0)
                {
                    stampIn.Account = userAccount;
                    stampIn.TimeStampYear = timeStampYear;
                    stampIn.TimeStampMonth = timeStampMonth;
                    stampIn.TimeStampDay = timeStampDay;
                    stampIn.TimeStampHour = timeStampHour;
                    stampIn.TimeStampMinute = timeStampMinute;
                    stampIn.TimeStampSecond = timeStampSecond;
                    stampIn.LastUpdate = DateTime.Now;
                    return await GetCurrentTimeStampModelService().AddTimeStampAsync(stampIn).ConfigureAwait(false);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public TimeStampModel StampOut(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampOut = new TimeStampModel();
                AccountModel userAccount = GetCurrentAccountModelService().GetAccountByGUID(userGUID);
                if (userAccount?.ID > 0)
                {
                    stampOut.Account = userAccount;
                    stampOut.TimeStampYear = timeStampYear;
                    stampOut.TimeStampMonth = timeStampMonth;
                    stampOut.TimeStampDay = timeStampDay;
                    stampOut.TimeStampHour = timeStampHour;
                    stampOut.TimeStampMinute = timeStampMinute;
                    stampOut.TimeStampSecond = timeStampSecond;
                    stampOut.LastUpdate = DateTime.Now;
                    return GetCurrentTimeStampModelService().AddTimeStamp(stampOut);
                }
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampOutAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {

            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampOut = new TimeStampModel();
                AccountModel userAccount = GetCurrentAccountModelService().GetAccountByGUID(userGUID);
                if (userAccount?.ID > 0)
                {
                    stampOut.Account = userAccount;
                    stampOut.TimeStampYear = timeStampYear;
                    stampOut.TimeStampMonth = timeStampMonth;
                    stampOut.TimeStampDay = timeStampDay;
                    stampOut.TimeStampHour = timeStampHour;
                    stampOut.TimeStampMinute = timeStampMinute;
                    stampOut.TimeStampSecond = timeStampSecond;
                    stampOut.LastUpdate = DateTime.Now;
                    return await GetCurrentTimeStampModelService().AddTimeStampAsync(stampOut).ConfigureAwait(false);
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
