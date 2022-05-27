using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class AccountRightSQLRepository : IAccountRightSQLRepository
    {
        public AccountRightSQLRepository()
        { }

        public IAccountRightModel AddAccountRightToDataSource(IAccountRightModel newAccountRight)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountRightModel insertEntry = new AccountRightModel()
                        {
                            RightID = newAccountRight.RightID,
                            Account = newAccountRight.Account,
                            LastUpdate = newAccountRight.LastUpdate,
                        };
                        sqlContext.AccountRight.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return insertEntry;
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.AddAccountRightToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountRightSQLRepository.AddAccountRightToDataSource(): {ex.Message}");
                return null;
            }
        }

        public Task<IAccountRightModel> AddAccountRightToDataSource_Async(IAccountRightModel newAccountRight)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountRightModel insertEntry = new AccountRightModel()
                        {
                            RightID = newAccountRight.RightID,
                            Account = newAccountRight.Account,
                            LastUpdate = newAccountRight.LastUpdate,
                        };
                        sqlContext.AccountRight.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return Task.FromResult<IAccountRightModel>(insertEntry);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.AddAccountRightToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountRightSQLRepository.AddAccountRightToDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public IAccountRightModel GetAccountRightByIDFromDataSource(int searchAccountRightID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.AccountRight.Find(searchAccountRightID);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.GetAccountRightByIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountRightSQLRepository.GetAccountRightByIDFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public Task<IAccountRightModel> GetAccountRightByIDFromDataSource_Async(int searchAccountRightID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return Task.FromResult<IAccountRightModel>(sqlContext.AccountRight.Find(searchAccountRightID));
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.GetAccountRightByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountRightSQLRepository.GetAccountRightByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public IAccountRightModel UpdateAccountRightToDataSource(IAccountRightModel updateAccountRight)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        IAccountRightModel existingAccountRight = sqlContext.AccountRight.Find(updateAccountRight.ID);
                        if (existingAccountRight != null)
                        {
                            existingAccountRight.ID = updateAccountRight.ID;
                            existingAccountRight.RightID = updateAccountRight.RightID;
                            existingAccountRight.Account = updateAccountRight.Account;
                            existingAccountRight.LastUpdate = updateAccountRight.LastUpdate;
                            sqlContext.SaveChanges();
                            return existingAccountRight;
                        }
                        else
                            return AddAccountRightToDataSource(updateAccountRight);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.UpdateAccountRightToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountRightSQLRepository.UpdateAccountRightToDataSource(): {ex.Message}");
                return null;
            }
        }

        public Task<IAccountRightModel> UpdateAccountRightToDataSource_Async(IAccountRightModel updateAccountRight)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        IAccountRightModel existingAccountRight = sqlContext.AccountRight.Find(updateAccountRight.ID);
                        if (existingAccountRight != null)
                        {
                            existingAccountRight.ID = updateAccountRight.ID;
                            existingAccountRight.RightID = updateAccountRight.RightID;
                            existingAccountRight.Account = updateAccountRight.Account;
                            existingAccountRight.LastUpdate = updateAccountRight.LastUpdate;
                            sqlContext.SaveChanges();
                            return Task.FromResult(existingAccountRight);
                        }
                        else
                            return AddAccountRightToDataSource_Async(updateAccountRight);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.UpdateAccountRightToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountRightSQLRepository.UpdateAccountRightToDataSource_Async(): {ex.Message}");
                return null;
            }
        }
    }
}
