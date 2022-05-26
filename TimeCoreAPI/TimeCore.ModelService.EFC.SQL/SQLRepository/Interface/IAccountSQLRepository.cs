using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IAccountSQLRepository
    {
        Task<IAccountModel> AddAccountToDataSource_Async(IAccountModel newAccount);
        IAccountModel AddAccountToDataSource(IAccountModel newAccount);
        Task<IAccountModel> GetAccountByIDFromDataSource_Async(int searchAccountID);
        IAccountModel GetAccountByIDFromDataSource(int searchAccountID);
        Task<IAccountModel> UpdateAccountToDataSource_Async(IAccountModel updateAccount);
        IAccountModel UpdateAccountToDataSource(IAccountModel updateAccount);
    }
}
