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

        public TimeCoreModelService(eDatabaseType selectedDatabaseType,
            ITimeStampModelService selectedTimeStampModelService,
            IAccountModelService selectedAccountModelService)
        {
            modelDatabaseType = selectedDatabaseType;
            timeStampModelService = selectedTimeStampModelService;
            accountModelService = selectedAccountModelService;
        }

        public List<TimeStampModel> GetStampTimesMonthList(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return timeStampModelService.GetTimeStampList(userAccount, selectedYear, selectedMonth);
            }
            else
                return null;
        }

        public async Task<List<TimeStampModel>> GetStampTimesMonthList_Async(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await timeStampModelService.GetTimeStampList_Async(userAccount, selectedYear, selectedMonth).ConfigureAwait(false);
            }
            else
                return null;
        }

        public AccountModel Login(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return accountModelService.GetAccountByCredentials(accountUserName, accountPassword, workshopID);
            }
            else
                return null;
        }

        public async Task<AccountModel> Login_Async(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await accountModelService.GetAccountByCredentials_Async(accountUserName, accountPassword, workshopID).ConfigureAwait(false);
            }
            else
                return null;
        }

        public TimeStampModel StampIn(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampIn = new TimeStampModel();
                stampIn.Account = userAccount;
                stampIn.TimeStampYear = timeStampYear;
                stampIn.TimeStampMonth = timeStampMonth;
                stampIn.TimeStampDay = timeStampDay;
                stampIn.TimeStampHour = timeStampHour;
                stampIn.TimeStampMinute = timeStampMinute;
                stampIn.TimeStampSecond = timeStampSecond;
                stampIn.LastUpdate = DateTime.Now;
                return timeStampModelService.AddTimeStamp(stampIn);
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampIn_Async(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampIn = new TimeStampModel();
                DateTime stampTime = DateTime.Now;
                stampIn.Account = userAccount;
                stampIn.TimeStampYear = timeStampYear;
                stampIn.TimeStampMonth = timeStampMonth;
                stampIn.TimeStampDay = timeStampDay;
                stampIn.TimeStampHour = timeStampHour;
                stampIn.TimeStampMinute = timeStampMinute;
                stampIn.TimeStampSecond = timeStampSecond;
                stampIn.LastUpdate = DateTime.Now;
                return await timeStampModelService.AddTimeStamp_Async(stampIn).ConfigureAwait(false);
            }
            else
                return null;
        }

        public TimeStampModel StampOut(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampOut = new TimeStampModel();
                stampOut.Account = userAccount;
                stampOut.TimeStampYear = timeStampYear;
                stampOut.TimeStampMonth = timeStampMonth;
                stampOut.TimeStampDay = timeStampDay;
                stampOut.TimeStampHour = timeStampHour;
                stampOut.TimeStampMinute = timeStampMinute;
                stampOut.TimeStampSecond = timeStampSecond;
                stampOut.LastUpdate = DateTime.Now;
                return timeStampModelService.AddTimeStamp(stampOut);
            }
            else
                return null;
        }

        public async Task<TimeStampModel> StampOut_Async(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond)
        {

            if (modelDatabaseType == eDatabaseType.SQL)
            {
                TimeStampModel stampOut = new TimeStampModel();
                stampOut.Account = userAccount;
                stampOut.TimeStampYear = timeStampYear;
                stampOut.TimeStampMonth = timeStampMonth;
                stampOut.TimeStampDay = timeStampDay;
                stampOut.TimeStampHour = timeStampHour;
                stampOut.TimeStampMinute = timeStampMinute;
                stampOut.TimeStampSecond = timeStampSecond;
                stampOut.LastUpdate = DateTime.Now;
                return await timeStampModelService.AddTimeStamp_Async(stampOut).ConfigureAwait(false);
            }
            else
                return null;
        }
    }
}
