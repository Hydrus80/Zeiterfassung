using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCore.ModelService.EFC.SQL;
using static TimeCore.ModelService.SupportedDatabaseType;

namespace TimeCore.ModelService
{
    public class TimeStampModelService : ITimeStampModelService
    {
        public eDatabaseType modelDatabaseType;
        public ITimeStampSQLRepository timeStapSQLRepository;

        public TimeStampModelService(eDatabaseType selectedDatabaseType,
            ITimeStampSQLRepository selectTimeStampSQLRepository)
        {
            modelDatabaseType = selectedDatabaseType;
            timeStapSQLRepository = selectTimeStampSQLRepository;
        }

        public TimeStampModel AddTimeStamp(TimeStampModel newTimeStamp)
        {
            return timeStapSQLRepository.AddTimeStampToDataSource(newTimeStamp);
        }

        public async Task<TimeStampModel> AddTimeStamp_Async(TimeStampModel newTimeStamp)
        {
            return await timeStapSQLRepository.AddTimeStampToDataSource_Async(newTimeStamp).ConfigureAwait(false);
        }

        public List<TimeStampModel> GetTimeStampList(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            return timeStapSQLRepository.GetTimeStampListFromDataSource(userAccount, selectedYear, selectedMonth);
        }

        public async Task<List<TimeStampModel>> GetTimeStampList_Async(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            return await timeStapSQLRepository.GetTimeStampListFromDataSource_Async(userAccount, selectedYear, selectedMonth).ConfigureAwait(false);
        }
    }
}
