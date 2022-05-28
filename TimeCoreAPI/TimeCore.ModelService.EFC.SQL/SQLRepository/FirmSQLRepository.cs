using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class FirmSQLRepository : IFirmSQLRepository
    {
        public FirmSQLRepository()
        { }

        public FirmModel AddFirmToDataSource(FirmModel newFirm)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        FirmModel insertEntry = new FirmModel()
                        {
                            Name = newFirm.Name,
                            Number = newFirm.Number,
                            LastUpdate = newFirm.LastUpdate,
                        };
                        sqlContext.Firm.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return insertEntry;
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.AddFirmToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"FirmSQLRepository.AddFirmToDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<FirmModel> AddFirmToDataSource_Async(FirmModel newFirm)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        FirmModel insertEntry = new FirmModel()
                        {
                            Name = newFirm.Name,
                            Number = newFirm.Number,
                            LastUpdate = newFirm.LastUpdate,
                        };
                        sqlContext.Firm.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return await Task.FromResult<FirmModel>(insertEntry).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.AddFirmToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"FirmSQLRepository.AddFirmToDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public FirmModel GetFirmByIDFromDataSource(int searchFirmID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.Firm.Find(searchFirmID);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.GetFirmByIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"FirmSQLRepository.GetFirmByIDFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<FirmModel> GetFirmByIDFromDataSource_Async(int searchFirmID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return await Task.FromResult<FirmModel>(sqlContext.Firm.Find(searchFirmID)).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.GetFirmByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"FirmSQLRepository.GetFirmByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public FirmModel UpdateFirmToDataSource(FirmModel updateFirm)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        FirmModel existingFirm = sqlContext.Firm.Find(updateFirm.ID);
                        if (existingFirm != null)
                        {
                            existingFirm.ID = updateFirm.ID;
                            existingFirm.Name = updateFirm.Name;
                            existingFirm.Number = updateFirm.Number;
                            existingFirm.LastUpdate = updateFirm.LastUpdate;
                            sqlContext.SaveChanges();
                            return existingFirm;
                        }
                        else
                            return AddFirmToDataSource(updateFirm);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.UpdateFirmToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"FirmSQLRepository.UpdateFirmToDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<FirmModel> UpdateFirmToDataSource_Async(FirmModel updateFirm)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        FirmModel existingFirm = sqlContext.Firm.Find(updateFirm.ID);
                        if (existingFirm != null)
                        {
                            existingFirm.ID = updateFirm.ID;
                            existingFirm.Name = updateFirm.Name;
                            existingFirm.Number = updateFirm.Number;
                            existingFirm.LastUpdate = updateFirm.LastUpdate;
                            sqlContext.SaveChanges();
                            return await Task.FromResult(existingFirm).ConfigureAwait(false);
                        }
                        else
                            return await AddFirmToDataSource_Async(updateFirm).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.UpdateFirmToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"FirmSQLRepository.UpdateFirmToDataSource_Async(): {ex.Message}");
                return null;
            }
        }
    }
}
