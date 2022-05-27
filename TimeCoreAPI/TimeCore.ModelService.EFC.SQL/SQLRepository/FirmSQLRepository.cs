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

        public IFirmModel AddFirmToDataSource(IFirmModel newFirm)
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

        public Task<IFirmModel> AddFirmToDataSource_Async(IFirmModel newFirm)
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
                        return Task.FromResult<IFirmModel>(insertEntry);
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

        public IFirmModel GetFirmByIDFromDataSource(int searchFirmID)
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

        public Task<IFirmModel> GetFirmByIDFromDataSource_Async(int searchFirmID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return Task.FromResult<IFirmModel>(sqlContext.Firm.Find(searchFirmID));
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

        public IFirmModel UpdateFirmToDataSource(IFirmModel updateFirm)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        IFirmModel existingFirm = sqlContext.Firm.Find(updateFirm.ID);
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

        public Task<IFirmModel> UpdateFirmToDataSource_Async(IFirmModel updateFirm)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        IFirmModel existingFirm = sqlContext.Firm.Find(updateFirm.ID);
                        if (existingFirm != null)
                        {
                            existingFirm.ID = updateFirm.ID;
                            existingFirm.Name = updateFirm.Name;
                            existingFirm.Number = updateFirm.Number;
                            existingFirm.LastUpdate = updateFirm.LastUpdate;
                            sqlContext.SaveChanges();
                            return Task.FromResult(existingFirm);
                        }
                        else
                            return AddFirmToDataSource_Async(updateFirm);
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
