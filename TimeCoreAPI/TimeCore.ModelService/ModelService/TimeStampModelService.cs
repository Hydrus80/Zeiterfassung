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

        public async Task<TimeStampModel> AddTimeStampAsync(TimeStampModel newTimeStamp)
        {
            return await timeStapSQLRepository.AddTimeStampToDataSourceAsync(newTimeStamp).ConfigureAwait(false);
        }

        public List<TimeStampModel> GetTimeStampList(string userGUID, int selectedYear, int selectedMonth)
        {
            return timeStapSQLRepository.GetTimeStampListFromDataSource(userGUID, selectedYear, selectedMonth);
        }

        public async Task<List<TimeStampModel>> GetTimeStampListAsync(string userGUID, int selectedYear, int selectedMonth)
        {
            return await timeStapSQLRepository.GetTimeStampListFromDataSourceAsync(userGUID, selectedYear, selectedMonth).ConfigureAwait(false);
        }
    }
}
