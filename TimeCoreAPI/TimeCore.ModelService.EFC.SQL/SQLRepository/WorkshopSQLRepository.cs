using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class WorkshopSQLRepository : IWorkshopSQLRepository
    {
        public WorkshopSQLRepository()
        { }

        public WorkshopModel AddWorkshopToDataSource(WorkshopModel newWorkshop)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        WorkshopModel insertEntry = new WorkshopModel()
                        {
                            Name = newWorkshop.Name,
                            Number = newWorkshop.Number,
                            Firm = newWorkshop.Firm,
                            LastUpdate = newWorkshop.LastUpdate,
                        };
                        sqlContext.Workshop.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return insertEntry;
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.AddWorkshopToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"WorkshopSQLRepository.AddWorkshopToDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<WorkshopModel> AddWorkshopToDataSource_Async(WorkshopModel newWorkshop)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        WorkshopModel insertEntry = new WorkshopModel()
                        {
                            Name = newWorkshop.Name,
                            Number = newWorkshop.Number,
                            Firm = newWorkshop.Firm,
                            LastUpdate = newWorkshop.LastUpdate,
                        };
                        sqlContext.Workshop.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return await Task.FromResult<WorkshopModel>(insertEntry).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.AddWorkshopToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"WorkshopSQLRepository.AddWorkshopToDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public WorkshopModel GetWorkshopByIDFromDataSource(int searchWorkshopID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.Workshop.Find(searchWorkshopID);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.GetWorkshopByIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }           
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"WorkshopSQLRepository.GetWorkshopByIDFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<WorkshopModel> GetWorkshopByIDFromDataSource_Async(int searchWorkshopID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return await Task.FromResult<WorkshopModel>(sqlContext.Workshop.Find(searchWorkshopID)).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.GetWorkshopByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"WorkshopSQLRepository.GetWorkshopByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public WorkshopModel UpdateWorkshopToDataSource(WorkshopModel updateWorkshop)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        WorkshopModel existingWorkshop = sqlContext.Workshop.Find(updateWorkshop.ID);
                        if (existingWorkshop != null)
                        {
                            existingWorkshop.ID = updateWorkshop.ID;
                            existingWorkshop.Name = updateWorkshop.Name;
                            existingWorkshop.Number = updateWorkshop.Number;
                            existingWorkshop.Firm = updateWorkshop.Firm;
                            existingWorkshop.LastUpdate = updateWorkshop.LastUpdate;
                            sqlContext.SaveChanges();
                            return existingWorkshop;
                        }
                        else
                            return AddWorkshopToDataSource(updateWorkshop);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.UpdateWorkshopToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"WorkshopSQLRepository.UpdateWorkshopToDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<WorkshopModel> UpdateWorkshopToDataSource_Async(WorkshopModel updateWorkshop)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        WorkshopModel existingWorkshop = sqlContext.Workshop.Find(updateWorkshop.ID);
                        if (existingWorkshop != null)
                        {
                            existingWorkshop.ID = updateWorkshop.ID;
                            existingWorkshop.Name = updateWorkshop.Name;
                            existingWorkshop.Number = updateWorkshop.Number;
                            existingWorkshop.Firm = updateWorkshop.Firm;
                            existingWorkshop.LastUpdate = updateWorkshop.LastUpdate;
                            sqlContext.SaveChanges();
                            return await Task.FromResult(existingWorkshop).ConfigureAwait(false);
                        }
                        else
                            return await AddWorkshopToDataSource_Async(updateWorkshop).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.UpdateWorkshopToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"WorkshopSQLRepository.UpdateWorkshopToDataSource_Async(): {ex.Message}");
                return null;
            }
        }
    }
}
