using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface ITimeCoreModulService
    {
        string Login(string accountUserName, string accountPassword);
        Task<string> LoginAsync(string accountUserName, string accountPassword);
        AccountModel Login(string accountGUID);
        Task<AccountModel> LoginAsync(string accountGUID);
        TimeStampModel StampIn(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        Task<TimeStampModel> StampInAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        TimeStampModel StampOut(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        Task<TimeStampModel> StampOutAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        List<TimeStampModel> GetStampTimesList(string userGUID, int selectedYear, int selectedMonth, int selectedDay);
        Task<List<TimeStampModel>> GetStampTimesMonthListAsync(string userGUID, int selectedYear, int selectedMonth, int selectedDay);
    }
}
