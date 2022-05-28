using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IAccountRightSQLRepository
    {
        Task<AccountRightModel> AddAccountRightToDataSource_Async(AccountRightModel newAccountRight);
        AccountRightModel AddAccountRightToDataSource(AccountRightModel newAccountRight);
        Task<AccountRightModel> GetAccountRightByIDFromDataSource_Async(int searchAccountRightID);
        AccountRightModel GetAccountRightByIDFromDataSource(int searchAccountRightID);
        Task<AccountRightModel> UpdateAccountRightToDataSource_Async(AccountRightModel updateAccountRight);
        AccountRightModel UpdateAccountRightToDataSource(AccountRightModel updateAccountRight);
    }
}
