using Model;
using System.Threading.Tasks;
using TimeCore.ModelService.EFC.SQL;

namespace TimeCore.ModelService
{
    public interface IFirmModelService
    {
        IFirmModel AddFirm(IFirmModel newFirm);
        Task<IFirmModel> AddFirm_Async(IFirmModel newFirm);
        IFirmSQLRepository GetCurrentFirmSQLRepository();
        IFirmModel GetFirmByID(int searchFirmID);
        Task<IFirmModel> GetFirmByID_Async(int searchFirmID);
        IFirmModel UpdateFirm(IFirmModel updateFirm);
        Task<IFirmModel> UpdateFirm_Async(IFirmModel updateFirm);
    }
}