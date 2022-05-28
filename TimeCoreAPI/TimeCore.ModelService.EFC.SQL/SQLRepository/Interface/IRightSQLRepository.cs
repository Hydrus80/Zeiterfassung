using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IRightSQLRepository
    {
        void DataSeeding();
        Task<RightModel> GetRightByIDFromDataSource_Async(int searchID);
        RightModel GetRightByIDFromDataSource(int searchID);

        Task<RightModel> GetRightByRightIDFromDataSource_Async(int searchRightID);
        RightModel GetRightByRightIDFromDataSource(int searchRightID);

    }
}
