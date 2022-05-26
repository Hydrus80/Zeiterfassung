using Model;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface IAccountModulService
    {
        Task<IAccountModel> GetAccountByNumber_Async(int searchAccountID);
        Task<IAccountModel> CreateAccount_Async(IAccountModel newAccount);
        Task<IAccountModel> UpdateAccount_Async(IAccountModel newAccount);
        IAccountModel GetAccountByNumber(int searchAccountID);
        IAccountModel CreateAccount(IAccountModel newAccount);
        IAccountModel UpdateAccount(IAccountModel newAccount);
    }
}
