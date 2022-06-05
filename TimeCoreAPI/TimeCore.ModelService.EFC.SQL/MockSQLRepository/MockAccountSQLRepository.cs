using Model;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCore.ModelService.EFC.SQL
{
    public class MockAccountSQLRepository : IAccountSQLRepository
    {
        //Felder
        MockData mockData = new MockData();

        public MockAccountSQLRepository()
        { }

        public AccountModel GetAccountByCredentialsFromDataSource(string accountUserName, string accountPassword)
        {
            AccountModel returnValue = mockData.GetAccounts().Where(s => s.Username == accountUserName &&
                        s.Password == accountPassword).FirstOrDefault();
            if(returnValue is null)
                returnValue = new AccountModel();
            return returnValue;
        }

        public async Task<AccountModel> GetAccountByCredentialsFromDataSourceAsync(string accountUserName, string accountPassword)
        {
            return await Task.FromResult(GetAccountByCredentialsFromDataSource(accountUserName, accountPassword)).ConfigureAwait(false);
        }

        public AccountModel GetAccountByGUIDFromDataSource(string accountGUID)
        {
            AccountModel returnValue = mockData.GetAccounts().Where(s => s.GUID == accountGUID).FirstOrDefault();
            if (returnValue is null)
                returnValue = new AccountModel();
            return returnValue;
        }

        public async Task<AccountModel> GetAccountByGUIDFromDataSourceAsync(string accountGUID)
        {
            return await Task.FromResult(GetAccountByGUIDFromDataSource(accountGUID)).ConfigureAwait(false);
        }
    }
}
