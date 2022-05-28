using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface ITimeCoreModulService
    {
        AccountModel Login(string accountUserName, string accountPassword, int workshopID);
        Task<AccountModel> Login_Async(string accountUserName, string accountPassword, int workshopID);
        TimeStampModel StampIn(AccountModel userAccount);
        Task<TimeStampModel> StampIn_Async(AccountModel userAccount);
        TimeStampModel StampOut(AccountModel userAccount);
        Task<TimeStampModel> StampOut_Async(AccountModel userAccount);
        List<TimeStampModel> GetStampTimesMonthList(AccountModel userAccount, int selectedMonth);
        Task<List<TimeStampModel>> GetStampTimesMonthList_Async(AccountModel userAccount, int selectedMonth);
    }
}
