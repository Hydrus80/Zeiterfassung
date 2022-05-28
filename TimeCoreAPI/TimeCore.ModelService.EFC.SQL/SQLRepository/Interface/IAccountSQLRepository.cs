using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IAccountSQLRepository
    {
        Task<AccountModel> AddAccountToDataSource_Async(AccountModel newAccount);
        AccountModel AddAccountToDataSource(AccountModel newAccount);
        Task<AccountModel> GetAccountByIDFromDataSource_Async(int searchAccountID);
        AccountModel GetAccountByCredentialsFromDataSource(string accountUserName, string accountPassword, int workshopID);
        Task<AccountModel> GetAccountByCredentialsFromDataSource_Async(string accountUserName, string accountPassword, int workshopID);
        AccountModel GetAccountByIDFromDataSource(int searchAccountID);
        Task<AccountModel> UpdateAccountToDataSource_Async(AccountModel updateAccount);
        AccountModel UpdateAccountToDataSource(AccountModel updateAccount);
    }
}
