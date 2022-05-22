using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService
{
    public interface IFirmRepository
    {
        Task<IFirmModel> AddFirmToDataSource_Async(IFirmModel newFirm);
        IFirmModel AddFirmToDataSource(IFirmModel newFirm);
        Task<IFirmModel> GetFirmByIDFromDataSource_Async(int searchFirmID);
        IFirmModel GetFirmByIDFromDataSource(int searchFirmID);       
    }
}
