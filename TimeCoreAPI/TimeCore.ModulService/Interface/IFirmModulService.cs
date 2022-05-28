using Model;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface IFirmModulService
    {
        Task<FirmModel> GetFirmByNumber_Async(int searchFirmID);
        Task<FirmModel> CreateFirm_Async(FirmModel newFirm);
        Task<FirmModel> UpdateFirm_Async(FirmModel newFirm);
        FirmModel GetFirmByNumber(int searchFirmID);
        FirmModel CreateFirm(FirmModel newFirm);
        FirmModel UpdateFirm(FirmModel newFirm);
    }
}
