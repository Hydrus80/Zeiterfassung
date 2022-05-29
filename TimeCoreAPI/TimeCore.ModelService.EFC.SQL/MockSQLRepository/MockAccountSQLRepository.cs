using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class MockAccountSQLRepository : IAccountSQLRepository
    {
        //Felder
        MockData mockData = new MockData();

        public MockAccountSQLRepository()
        { }

        public AccountModel GetAccountByCredentialsFromDataSource(string accountUserName, string accountPassword, int workshopID)
        {
            AccountModel returnValue = mockData.GetAccounts().Where(s => s.Username == accountUserName &&
                        s.Password == accountPassword &&
                        s.Workshop.ID == workshopID).FirstOrDefault();
            if(returnValue is null)
                returnValue = new AccountModel();
            return returnValue;
        }

        public async Task<AccountModel> GetAccountByCredentialsFromDataSource_Async(string accountUserName, string accountPassword, int workshopID)
        {
            return await Task.FromResult(GetAccountByCredentialsFromDataSource(accountUserName, accountPassword, workshopID)).ConfigureAwait(false);
        }

    }
}
