﻿using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface ITimeCoreModulService
    {
        string Login(string accountUserName, string accountPassword);
        Task<string> LoginAsync(string accountUserName, string accountPassword);
        TimeStampModel StampIn(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        Task<TimeStampModel> StampInAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        TimeStampModel StampOut(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        Task<TimeStampModel> StampOutAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        List<TimeStampModel> GetStampTimesMonthList(string userGUID, int selectedYear, int selectedMonth);
        Task<List<TimeStampModel>> GetStampTimesMonthListAsync(string userGUID, int selectedYear, int selectedMonth);
    }
}
