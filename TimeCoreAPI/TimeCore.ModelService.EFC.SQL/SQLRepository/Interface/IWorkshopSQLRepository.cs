using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IWorkshopSQLRepository
    {
        Task<IWorkshopModel> AddWorkshopToDataSource_Async(IWorkshopModel newWorkshop);
        IWorkshopModel AddWorkshopToDataSource(IWorkshopModel newWorkshop);
        Task<IWorkshopModel> GetWorkshopByIDFromDataSource_Async(int searchWorkshopID);
        IWorkshopModel GetWorkshopByIDFromDataSource(int searchWorkshopID);
        Task<IWorkshopModel> UpdateWorkshopToDataSource_Async(IWorkshopModel updateWorkshop);
        IWorkshopModel UpdateWorkshopToDataSource(IWorkshopModel updateWorkshop);
    }
}
