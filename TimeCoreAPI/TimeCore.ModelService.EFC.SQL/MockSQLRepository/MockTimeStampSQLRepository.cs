﻿using Model;
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

        public async Task<TimeStampModel> AddTimeStampToDataSource_Async(TimeStampModel newTimeStamp)
        {
            return await Task.FromResult(AddTimeStampToDataSource(newTimeStamp)).ConfigureAwait(false);
        }

        public List<TimeStampModel> GetTimeStampListFromDataSource(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            List<TimeStampModel> returnList = mockData.GetTimeStampsForAccountOne().Where(s => s.TimeStampYear == selectedYear &&
                        s.TimeStampMonth == selectedMonth &&
                        s.Account.ID == userAccount.ID &&
                        s.Account.Password == userAccount.Password).ToList();
            if (returnList is null)
                returnList = new List<TimeStampModel>();
            return returnList;
        }

        public async Task<List<TimeStampModel>> GetTimeStampListFromDataSource_Async(AccountModel userAccount, int selectedYear, int selectedMonth)
        {
            return await Task.FromResult(GetTimeStampListFromDataSource(userAccount, selectedYear, selectedMonth)).ConfigureAwait(false);
        }
    }
}