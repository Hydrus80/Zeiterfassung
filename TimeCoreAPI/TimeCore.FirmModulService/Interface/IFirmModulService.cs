using Model;
using System.Threading.Tasks;

namespace TimeCore.FirmModulService
{
    public interface IFirmModulService
    {
        Task<IFirmModel> GetFirmByNumber_Async(int searchFirmID);
        Task<IFirmModel> CreateFirm_Async(IFirmModel newFirm);
        IFirmModel GetFirmByNumber(int searchFirmID);
        IFirmModel CreateFirm(IFirmModel newFirm);
    }
}
