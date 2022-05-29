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

        public TimeCoreModulService(eDatabaseType selectedDatabaseType, 
            IAccountSQLRepository selectAccountSQLRepository, ITimeStampSQLRepository selectTimeStampSQLRepository)
        {
            modelDatabaseType = selectedDatabaseType;
            accountSQLRepository = selectAccountSQLRepository;
            timeStampSQLRepository = selectTimeStampSQLRepository;
        }

        public List<TimeStampModel> GetStampTimesMonthList(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return timeStampSQLRepository.GetTimeStampListFromDataSource(userAccount, selectedYear, selectedMonth);
            }
            else
                return null;
        }

        public async Task<List<TimeStampModel>> GetStampTimesMonthList_Async(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await timeStampSQLRepository.GetTimeStampListFromDataSource_Async(userAccount, selectedYear, selectedMonth).ConfigureAwait(false);
            }
            else
                return null;
        }

        public AccountModel Login(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return accountSQLRepository.GetAccountByCredentialsFromDataSource(accountUserName, accountPassword, workshopID);
            }
            else
                return null;
        }

        public async Task<AccountModel> Login_Async(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await accountSQLRepository.GetAccountByCredentialsFromDataSource_Async(accountUserName, accountPassword, workshopID).ConfigureAwait(false);
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
                return timeStampSQLRepository.AddTimeStampToDataSource(stampIn);
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
                return await timeStampSQLRepository.AddTimeStampToDataSource_Async(stampIn).ConfigureAwait(false);
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
                return timeStampSQLRepository.AddTimeStampToDataSource(stampOut);
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
                return await timeStampSQLRepository.AddTimeStampToDataSource_Async(stampOut).ConfigureAwait(false);
            }
            else
                return null;
        }
    }
}
