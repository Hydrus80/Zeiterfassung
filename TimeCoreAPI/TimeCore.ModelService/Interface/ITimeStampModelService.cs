using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModelService
{
    public interface ITimeStampModelService
    {
        TimeStampModel AddTimeStamp(TimeStampModel newTimeStamp);
        Task<TimeStampModel> AddTimeStamp_Async(TimeStampModel newTimeStamp);
        List<TimeStampModel> GetTimeStampList(AccountModel userAccount, int selectedYear, int selectedMonth);
        Task<List<TimeStampModel>> GetTimeStampList_Async(AccountModel userAccount, int selectedYear, int selectedMonth);
    }
}