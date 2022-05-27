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

        public IWorkshopModel AddWorkshopToDataSource(IWorkshopModel newWorkshop)
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

        public Task<IWorkshopModel> AddWorkshopToDataSource_Async(IWorkshopModel newWorkshop)
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
                        return Task.FromResult<IWorkshopModel>(insertEntry);
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

        public IWorkshopModel GetWorkshopByIDFromDataSource(int searchWorkshopID)
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

        public Task<IWorkshopModel> GetWorkshopByIDFromDataSource_Async(int searchWorkshopID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return Task.FromResult<IWorkshopModel>(sqlContext.Workshop.Find(searchWorkshopID));
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

        public IWorkshopModel UpdateWorkshopToDataSource(IWorkshopModel updateWorkshop)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        IWorkshopModel existingWorkshop = sqlContext.Workshop.Find(updateWorkshop.ID);
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

        public Task<IWorkshopModel> UpdateWorkshopToDataSource_Async(IWorkshopModel updateWorkshop)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        IWorkshopModel existingWorkshop = sqlContext.Workshop.Find(updateWorkshop.ID);
                        if (existingWorkshop != null)
                        {
                            existingWorkshop.ID = updateWorkshop.ID;
                            existingWorkshop.Name = updateWorkshop.Name;
                            existingWorkshop.Number = updateWorkshop.Number;
                            existingWorkshop.Firm = updateWorkshop.Firm;
                            existingWorkshop.LastUpdate = updateWorkshop.LastUpdate;
                            sqlContext.SaveChanges();
                            return Task.FromResult(existingWorkshop);
                        }
                        else
                            return AddWorkshopToDataSource_Async(updateWorkshop);
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
