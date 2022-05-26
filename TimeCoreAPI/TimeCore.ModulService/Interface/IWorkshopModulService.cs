using Model;
using System.Threading.Tasks;

namespace TimeCore.ModulService
{
    public interface IWorkshopModulService
    {
        Task<IWorkshopModel> GetWorkshopByNumber_Async(int searchWorkshopID);
        Task<IWorkshopModel> CreateWorkshop_Async(IWorkshopModel newWorkshop);
        Task<IWorkshopModel> UpdateWorkshop_Async(IWorkshopModel newWorkshop);
        IWorkshopModel GetWorkshopByNumber(int searchWorkshopID);
        IWorkshopModel CreateWorkshop(IWorkshopModel newWorkshop);
        IWorkshopModel UpdateWorkshop(IWorkshopModel newWorkshop);
    }
}
