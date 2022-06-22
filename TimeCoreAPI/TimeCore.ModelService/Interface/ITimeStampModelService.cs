using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModelService
{
    public interface ITimeStampModelService
    {
        TimeStampModel AddTimeStamp(TimeStampModel newTimeStamp);
        Task<TimeStampModel> AddTimeStampAsync(TimeStampModel newTimeStamp);
        List<TimeStampModel> GetTimeStampList(string userGUID, int selectedYear, int selectedMonth, int selectedDay);
        Task<List<TimeStampModel>> GetTimeStampListAsync(string userGUID, int selectedYear, int selectedMonth, int selectedDay);
    }
}