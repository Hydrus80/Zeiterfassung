using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface ITimeStampSQLRepository
    {
        TimeStampModel AddTimeStampToDataSource(TimeStampModel newTimeStamp);
        Task<TimeStampModel> AddTimeStampToDataSourceAsync(TimeStampModel newTimeStamp);
        List<TimeStampModel> GetTimeStampListFromDataSource(string userGUID, int selectedYear, int selectedMonth);
        Task<List<TimeStampModel>> GetTimeStampListFromDataSourceAsync(string userGUID, int selectedYear, int selectedMonth);
    }
}