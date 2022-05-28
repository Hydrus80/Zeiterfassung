using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCore.ModelService.EFC.SQL;
using static TimeCore.ModelService.SupportedDatabaseType;

namespace TimeCore.ModulService
{
    public class TimeCoreModulService : ITimeCoreModulService
    {
        public eDatabaseType modelDatabaseType;
        public IAccountSQLRepository accountSQLRepository;
        public ITimeStampSQLRepository timeStampSQLRepository;

        public TimeCoreModulService(eDatabaseType selectedDatabaseType)
        {
            modelDatabaseType = selectedDatabaseType;
        }

        public IAccountSQLRepository GetCurrentAccountSQLRepository()
        {
            if (accountSQLRepository is null)
                accountSQLRepository = new AccountSQLRepository();
            return accountSQLRepository;
        }

        public ITimeStampSQLRepository GetCurrentTimeStampSQLRepository()
        {
            if (timeStampSQLRepository is null)
                timeStampSQLRepository = new TimeStampSQLRepository();
            return timeStampSQLRepository;
        }

        public List<TimeStampModel> GetStampTimesMonthList(AccountModel userAccount, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentTimeStampSQLRepository().GetTimeStampListFromDataSource(userAccount, selectedMonth);
            }
            else
                return null;
        }

        public async Task<List<TimeStampModel>> GetStampTimesMonthList_Async(AccountModel userAccount, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentTimeStampSQLRepository().GetTimeStampListFromDataSource_Async(userAccount, selectedMonth).ConfigureAwait(false);
            }
            else
                return null;
        }

        public AccountModel Login(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return GetCurrentAccountSQLRepository().GetAccountByCredentialsFromDataSource(accountUserName, accountPassword, workshopID);
            }
            else
                return null;
        }

        public async Task<AccountModel> Login_Async(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await GetCurrentAccountSQLRepository().GetAccountByCredentialsFromDataSource_Async(accountUserName, accountPassword, workshopID).ConfigureAwait(false);
            }
            else
                return null;
        }

        public TimeStampModel StampIn(AccountModel userAccount)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampIn = new TimeStampModel();
                DateTime stampTime = DateTime.Now;
                stampIn.Account = userAccount;
                stampIn.TimeStampYear = stampTime.Year;
                stampIn.TimeStampMonth = stampTime.Month;
                stampIn.TimeStampDay = stampTime.Day;
                stampIn.TimeStampHour = stampTime.Hour;
                stampIn.TimeStampMinute = stampTime.Minute;
                stampIn.TimeStampSecond = stampTime.Second;
                stampIn.LastUpdate = stampTime;
                return GetCurrentTimeStampSQLRepository().AddTimeStampToDataSource(stampIn);
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampIn_Async(AccountModel userAccount)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampIn = new TimeStampModel();
                DateTime stampTime = DateTime.Now;
                stampIn.Account = userAccount;
                stampIn.TimeStampYear = stampTime.Year;
                stampIn.TimeStampMonth = stampTime.Month;
                stampIn.TimeStampDay = stampTime.Day;
                stampIn.TimeStampHour = stampTime.Hour;
                stampIn.TimeStampMinute = stampTime.Minute;
                stampIn.TimeStampSecond = stampTime.Second;
                stampIn.LastUpdate = stampTime;
                return await Task.FromResult(GetCurrentTimeStampSQLRepository().AddTimeStampToDataSource(stampIn)).ConfigureAwait(false);
            }
            else
                return null;
        }

        public TimeStampModel StampOut(AccountModel userAccount)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampOut = new TimeStampModel();
                DateTime stampTime = DateTime.Now;
                stampOut.Account = userAccount;
                stampOut.TimeStampYear = stampTime.Year;
                stampOut.TimeStampMonth = stampTime.Month;
                stampOut.TimeStampDay = stampTime.Day;
                stampOut.TimeStampHour = stampTime.Hour;
                stampOut.TimeStampMinute = stampTime.Minute;
                stampOut.TimeStampSecond = stampTime.Second;
                stampOut.LastUpdate = stampTime;
                return GetCurrentTimeStampSQLRepository().AddTimeStampToDataSource(stampOut);
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampOut_Async(AccountModel userAccount)
        {

            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampOut = new TimeStampModel();
                DateTime stampTime = DateTime.Now;
                stampOut.Account = userAccount;
                stampOut.TimeStampYear = stampTime.Year;
                stampOut.TimeStampMonth = stampTime.Month;
                stampOut.TimeStampDay = stampTime.Day;
                stampOut.TimeStampHour = stampTime.Hour;
                stampOut.TimeStampMinute = stampTime.Minute;
                stampOut.TimeStampSecond = stampTime.Second;
                stampOut.LastUpdate = stampTime;
                return await Task.FromResult(GetCurrentTimeStampSQLRepository().AddTimeStampToDataSource(stampOut)).ConfigureAwait(false);
            }
            else
                return null;
        }
    }
}
