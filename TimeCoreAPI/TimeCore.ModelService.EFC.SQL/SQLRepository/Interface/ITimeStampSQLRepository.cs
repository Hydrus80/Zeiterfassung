using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface ITimeStampSQLRepository
    {
        TimeStampModel AddTimeStampToDataSource(TimeStampModel newTimeStamp);
        Task<TimeStampModel> AddTimeStampToDataSource_Async(TimeStampModel newTimeStamp);
        TimeStampModel GetTimeStampByIDFromDataSource(int searchTimeStampID);
        Task<TimeStampModel> GetTimeStampByIDFromDataSource_Async(int searchTimeStampID);
        TimeStampModel UpdateTimeStampToDataSource(TimeStampModel updateTimeStamp);
        Task<TimeStampModel> UpdateTimeStampToDataSource_Async(TimeStampModel updateTimeStamp);
        List<TimeStampModel> GetTimeStampListFromDataSource(AccountModel userAccount, int selectedYear, int selectedMonth);
        Task<List<TimeStampModel>> GetTimeStampListFromDataSource_Async(AccountModel userAccount, int selectedYear, int selectedMonth);
    }
}