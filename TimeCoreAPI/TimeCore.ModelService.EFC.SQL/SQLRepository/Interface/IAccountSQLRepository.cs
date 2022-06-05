using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IAccountSQLRepository
    {
        AccountModel GetAccountByCredentialsFromDataSource(string accountUserName, string accountPassword);
        Task<AccountModel> GetAccountByCredentialsFromDataSourceAsync(string accountUserName, string accountPassword);
        AccountModel GetAccountByGUIDFromDataSource(string accountGUID);
        Task<AccountModel> GetAccountByGUIDFromDataSourceAsync(string accountGUID);
    }
}
