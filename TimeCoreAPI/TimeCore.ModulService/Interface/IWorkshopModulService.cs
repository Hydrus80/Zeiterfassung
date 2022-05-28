using Model;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface IWorkshopModulService
    {
        Task<WorkshopModel> GetWorkshopByNumber_Async(int searchWorkshopID);
        Task<WorkshopModel> CreateWorkshop_Async(WorkshopModel newWorkshop);
        Task<WorkshopModel> UpdateWorkshop_Async(WorkshopModel newWorkshop);
        WorkshopModel GetWorkshopByNumber(int searchWorkshopID);
        WorkshopModel CreateWorkshop(WorkshopModel newWorkshop);
        WorkshopModel UpdateWorkshop(WorkshopModel newWorkshop);
    }
}
