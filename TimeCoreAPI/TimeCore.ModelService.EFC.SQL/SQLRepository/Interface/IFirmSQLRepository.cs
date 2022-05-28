using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IFirmSQLRepository
    {
        Task<FirmModel> AddFirmToDataSource_Async(FirmModel newFirm);
        FirmModel AddFirmToDataSource(FirmModel newFirm);
        Task<FirmModel> GetFirmByIDFromDataSource_Async(int searchFirmID);
        FirmModel GetFirmByIDFromDataSource(int searchFirmID);
        Task<FirmModel> UpdateFirmToDataSource_Async(FirmModel updateFirm);
        FirmModel UpdateFirmToDataSource(FirmModel updateFirm);
    }
}
