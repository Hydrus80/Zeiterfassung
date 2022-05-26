using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IFirmSQLRepository
    {
        Task<IFirmModel> AddFirmToDataSource_Async(IFirmModel newFirm);
        IFirmModel AddFirmToDataSource(IFirmModel newFirm);
        Task<IFirmModel> GetFirmByIDFromDataSource_Async(int searchFirmID);
        IFirmModel GetFirmByIDFromDataSource(int searchFirmID);
        Task<IFirmModel> UpdateFirmToDataSource_Async(IFirmModel newFirm);
        IFirmModel UpdateFirmToDataSource(IFirmModel newFirm);
    }
}
