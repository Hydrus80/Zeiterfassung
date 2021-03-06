using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModelService
{
    public interface ITimeCoreModelService
    {
        List<TimeStampModel> GetStampTimesList(string userGUID, int selectedYear, int selectedMonth, int selectedDay);
        Task<List<TimeStampModel>> GetStampTimesListAsync(string userGUID, int selectedYear, int selectedMonth, int selectedDay);
        AccountModel Authenticate(string accountUserName, string accountPassword);
        Task<AccountModel> AuthenticateAsync(string accountUserName, string accountPassword);
        AccountModel Authenticate(string accountGUID);
        Task<AccountModel> AuthenticateAsync(string accountGUID);
        TimeStampModel StampIn(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        Task<TimeStampModel> StampInAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        TimeStampModel StampOut(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
        Task<TimeStampModel> StampOutAsync(string userGUID, int timeStampYear, int timeStampMonth, int timeStampDay, int timeStampHour, int timeStampMinute, int timeStampSecond);
    }
}