using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ModelService.EFC.SQL;
using static TimeCore.ModelService.SupportedDatabaseType;

namespace TimeCore.ModelService
{
    public class FirmModelService : IFirmModelService
    {
        public eDatabaseType modelDatabaseType;
        public IFirmSQLRepository modelFirmSQLRepository;

        public FirmModelService(eDatabaseType selectedDatabaseType)
        {
            modelDatabaseType = selectedDatabaseType;
        }

        public IFirmSQLRepository GetCurrentFirmSQLRepository()
        {
            if (modelFirmSQLRepository is null)
                modelFirmSQLRepository = new FirmSQLRepository();
            return modelFirmSQLRepository;
        }

        public IFirmModel AddFirm(IFirmModel newFirm)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().AddFirmToDataSource(newFirm);
            else
                return null;
        }

        public Task<IFirmModel> AddFirm_Async(IFirmModel newFirm)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().AddFirmToDataSource_Async(newFirm);
            else
                return null;
        }

        public IFirmModel GetFirmByID(int searchFirmID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().GetFirmByIDFromDataSource(searchFirmID);
            else
                return null;
        }

        public Task<IFirmModel> GetFirmByID_Async(int searchFirmID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().GetFirmByIDFromDataSource_Async(searchFirmID);
            else
                return null;
        }

        public IFirmModel UpdateFirm(IFirmModel updateFirm)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().UpdateFirmToDataSource(updateFirm);
            else
                return null;
        }

        public Task<IFirmModel> UpdateFirm_Async(IFirmModel updateFirm)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().UpdateFirmToDataSource_Async(updateFirm);
            else
                return null;
        }
    }
}
