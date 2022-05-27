using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IAccountRightSQLRepository
    {
        Task<IAccountRightModel> AddAccountRightToDataSource_Async(IAccountRightModel newAccountRight);
        IAccountRightModel AddAccountRightToDataSource(IAccountRightModel newAccountRight);
        Task<IAccountRightModel> GetAccountRightByIDFromDataSource_Async(int searchAccountRightID);
        IAccountRightModel GetAccountRightByIDFromDataSource(int searchAccountRightID);
        Task<IAccountRightModel> UpdateAccountRightToDataSource_Async(IAccountRightModel updateAccountRight);
        IAccountRightModel UpdateAccountRightToDataSource(IAccountRightModel updateAccountRight);
    }
}
