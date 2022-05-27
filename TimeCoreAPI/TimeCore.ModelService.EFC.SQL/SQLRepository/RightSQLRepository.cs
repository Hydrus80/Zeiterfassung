using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class RightSQLRepository : IRightSQLRepository
    {
        public RightSQLRepository()
        { }

        public void DataSeeding()
        {
            //Context setzen
            using (SQLContext sqlContext = new SQLContext())
            {
                //Existiert Datenbank und ist der Zugriff gewährt?
                if (sqlContext.Database.CanConnect())
                {
                    //Zugriffsrechte setzen
                    var checkRight = sqlContext.Right.Where(b => b.RightID == (int)eRights.Administrator);
                    if (checkRight == null)
                        sqlContext.Right.Add(new RightModel { RightID = (int)eRights.Administrator, Description = Enum.GetName(typeof(eRights), eRights.Administrator) });
                    checkRight = sqlContext.Right.Where(b => b.RightID == (int)eRights.Buchhaltung);
                    if (checkRight == null)
                        sqlContext.Right.Add(new RightModel { RightID = (int)eRights.Buchhaltung, Description = Enum.GetName(typeof(eRights), eRights.Buchhaltung) });
                    checkRight = sqlContext.Right.Where(b => b.RightID == (int)eRights.Benutzer);
                    if (checkRight == null)
                        sqlContext.Right.Add(new RightModel { RightID = (int)eRights.Benutzer, Description = Enum.GetName(typeof(eRights), eRights.Benutzer) });

                    //Änderungen speichern
                    sqlContext.SaveChanges();
                }
                else
                {
                    ErrorHandlerLog.WriteError("RightSQLRepository.DataSeeding(): Keine Verbindung zur Datenbank möglich");
                }
            }
        }

        public IRightModel GetRightByIDFromDataSource(int searchID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.Right.Find(searchID);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("RightSQLRepository.GetRightByIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RightSQLRepository.GetRightByIDFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public Task<IRightModel> GetRightByIDFromDataSource_Async(int searchID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return Task.FromResult<IRightModel>(sqlContext.Right.Find(searchID));
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("RightSQLRepository.GetRightByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RightSQLRepository.GetRightByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public IRightModel GetRightByRightIDFromDataSource(int searchRightID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.Right.Where(b => b.RightID == searchRightID).FirstOrDefault();
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("RightSQLRepository.GetRightByIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RightSQLRepository.GetRightByIDFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public Task<IRightModel> GetRightByRightIDFromDataSource_Async(int searchRightID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return Task.FromResult<IRightModel>(sqlContext.Right.Where(b => b.RightID == searchRightID).FirstOrDefault());
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("RightSQLRepository.GetRightByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"RightSQLRepository.GetRightByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }
    }
}
