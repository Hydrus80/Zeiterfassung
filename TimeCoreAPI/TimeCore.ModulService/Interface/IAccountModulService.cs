using Model;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface IAccountModulService
    {
        Task<AccountModel> GetAccountByNumber_Async(int searchAccountID);
        Task<AccountModel> CreateAccount_Async(AccountModel newAccount);
        Task<AccountModel> UpdateAccount_Async(AccountModel newAccount);
        AccountModel GetAccountByNumber(int searchAccountID);
        AccountModel CreateAccount(AccountModel newAccount);
        AccountModel UpdateAccount(AccountModel newAccount);
    }
}
