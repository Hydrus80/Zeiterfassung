﻿using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface ITimeCoreModulService
    {
        AccountModel Login(string accountUserName, string accountPassword, int workshopID);
        Task<AccountModel> Login_Async(string accountUserName, string accountPassword, int workshopID);
        TimeStampModel StampIn(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        Task<TimeStampModel> StampIn_Async(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        TimeStampModel StampOut(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        Task<TimeStampModel> StampOut_Async(AccountModel userAccount, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        List<TimeStampModel> GetStampTimesMonthList(AccountModel userAccount, int selectedYear, int selectedMonth);
        Task<List<TimeStampModel>> GetStampTimesMonthList_Async(AccountModel userAccount, int selectedYear, int selectedMonth);
    }
}
