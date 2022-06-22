using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class MockTimeStampSQLRepository : ITimeStampSQLRepository
    {
        //Felder
        MockData mockData = new MockData();

        public MockTimeStampSQLRepository()
        { }

        public TimeStampModel AddTimeStampToDataSource(TimeStampModel newTimeStamp)
        {
            if (newTimeStamp.Account.ID == mockData.GetAccountOne().ID)
            {
                if (newTimeStamp.Account.Password == mockData.GetAccountOne().Password)
                    newTimeStamp.ID = 1;
            }
            if (newTimeStamp.Account.ID == mockData.GetAccountTwo().ID)
            {
                if (newTimeStamp.Account.Password == mockData.GetAccountTwo().Password)
                    newTimeStamp.ID = 1;
            }
            return newTimeStamp;
        }

        public async Task<TimeStampModel> AddTimeStampToDataSourceAsync(TimeStampModel newTimeStamp)
        {
            return await Task.FromResult(AddTimeStampToDataSource(newTimeStamp)).ConfigureAwait(false);
        }

        public List<TimeStampModel> GetTimeStampListFromDataSource(string userGUID, int selectedYear, int selectedMonth, int selectedDay)
        {
            List<TimeStampModel> returnList = mockData.GetTimeStampsForAccountOne().Where(s => s.TimeStampYear == selectedYear &&
                        s.TimeStampMonth == selectedMonth && 
                        s.TimeStampDay == selectedDay &&
                        s.Account.GUID == userGUID).ToList();
            if (returnList is null)
                returnList = new List<TimeStampModel>();
            return returnList;
        }

        public async Task<List<TimeStampModel>> GetTimeStampListFromDataSourceAsync(string userGUID, int selectedYear, int selectedMonth, int selectedDay)
        {
            return await Task.FromResult(GetTimeStampListFromDataSource(userGUID, selectedYear, selectedMonth, selectedDay)).ConfigureAwait(false);
        }
    }
}
