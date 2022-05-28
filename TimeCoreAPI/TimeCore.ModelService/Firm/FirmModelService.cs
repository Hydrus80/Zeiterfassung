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

        public FirmModel AddFirm(FirmModel newFirm)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().AddFirmToDataSource(newFirm);
            else
                return null;
        }

        public async Task<FirmModel> AddFirm_Async(FirmModel newFirm)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return await GetCurrentFirmSQLRepository().AddFirmToDataSource_Async(newFirm).ConfigureAwait(false);
            else
                return null;
        }

        public FirmModel GetFirmByID(int searchFirmID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().GetFirmByIDFromDataSource(searchFirmID);
            else
                return null;
        }

        public async Task<FirmModel> GetFirmByID_Async(int searchFirmID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return await GetCurrentFirmSQLRepository().GetFirmByIDFromDataSource_Async(searchFirmID).ConfigureAwait(false);
            else
                return null;
        }

        public FirmModel UpdateFirm(FirmModel updateFirm)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return GetCurrentFirmSQLRepository().UpdateFirmToDataSource(updateFirm);
            else
                return null;
        }

        public async Task<FirmModel> UpdateFirm_Async(FirmModel updateFirm)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
                return await GetCurrentFirmSQLRepository().UpdateFirmToDataSource_Async(updateFirm).ConfigureAwait(false);
            else
                return null;
        }
    }
}
