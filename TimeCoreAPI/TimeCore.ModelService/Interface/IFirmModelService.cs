using Model;
using System.Threading.Tasks;
using TimeCore.ModelService.EFC.SQL;

namespace TimeCore.ModelService
{
    public interface IFirmModelService
    {
        FirmModel AddFirm(FirmModel newFirm);
        Task<FirmModel> AddFirm_Async(FirmModel newFirm);
        IFirmSQLRepository GetCurrentFirmSQLRepository();
        FirmModel GetFirmByID(int searchFirmID);
        Task<FirmModel> GetFirmByID_Async(int searchFirmID);
        FirmModel UpdateFirm(FirmModel updateFirm);
        Task<FirmModel> UpdateFirm_Async(FirmModel updateFirm);
    }
}