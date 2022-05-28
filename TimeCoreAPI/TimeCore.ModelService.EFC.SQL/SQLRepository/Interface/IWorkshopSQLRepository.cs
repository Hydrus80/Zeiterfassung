using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IWorkshopSQLRepository
    {
        Task<WorkshopModel> AddWorkshopToDataSource_Async(WorkshopModel newWorkshop);
        WorkshopModel AddWorkshopToDataSource(WorkshopModel newWorkshop);
        Task<WorkshopModel> GetWorkshopByIDFromDataSource_Async(int searchWorkshopID);
        WorkshopModel GetWorkshopByIDFromDataSource(int searchWorkshopID);
        Task<WorkshopModel> UpdateWorkshopToDataSource_Async(WorkshopModel updateWorkshop);
        WorkshopModel UpdateWorkshopToDataSource(WorkshopModel updateWorkshop);
    }
}
