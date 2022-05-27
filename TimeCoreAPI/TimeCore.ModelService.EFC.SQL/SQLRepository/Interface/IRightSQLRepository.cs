using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IRightSQLRepository
    {
        void DataSeeding();
        Task<IRightModel> GetRightByIDFromDataSource_Async(int searchID);
        IRightModel GetRightByIDFromDataSource(int searchID);

        Task<IRightModel> GetRightByRightIDFromDataSource_Async(int searchRightID);
        IRightModel GetRightByRightIDFromDataSource(int searchRightID);

    }
}
