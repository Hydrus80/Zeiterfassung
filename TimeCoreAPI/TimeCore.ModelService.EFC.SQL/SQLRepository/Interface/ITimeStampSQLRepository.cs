using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface ITimeStampSQLRepository
    {
        TimeStampModel AddTimeStampToDataSource(TimeStampModel newTimeStamp);
        Task<TimeStampModel> AddTimeStampToDataSource_Async(TimeStampModel newTimeStamp);
        List<TimeStampModel> GetTimeStampListFromDataSource(AccountModel userAccount, int selectedYear, int selectedMonth);
        Task<List<TimeStampModel>> GetTimeStampListFromDataSource_Async(AccountModel userAccount, int selectedYear, int selectedMonth);
    }
}