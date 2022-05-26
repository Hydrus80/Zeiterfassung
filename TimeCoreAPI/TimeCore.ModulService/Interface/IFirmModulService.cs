using Model;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface IFirmModulService
    {
        Task<IFirmModel> GetFirmByNumber_Async(int searchFirmID);
        Task<IFirmModel> CreateFirm_Async(IFirmModel newFirm);
        Task<IFirmModel> UpdateFirm_Async(IFirmModel newFirm);
        IFirmModel GetFirmByNumber(int searchFirmID);
        IFirmModel CreateFirm(IFirmModel newFirm);
        IFirmModel UpdateFirm(IFirmModel newFirm);
    }
}
