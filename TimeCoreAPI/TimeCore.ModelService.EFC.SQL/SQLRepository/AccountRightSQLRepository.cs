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

        public AccountRightModel AddAccountRightToDataSource(AccountRightModel newAccountRight)
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

        public async Task<AccountRightModel> AddAccountRightToDataSource_Async(AccountRightModel newAccountRight)
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
                        return await Task.FromResult<AccountRightModel>(insertEntry).ConfigureAwait(false);
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

        public AccountRightModel GetAccountRightByIDFromDataSource(int searchAccountRightID)
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

        public async Task<AccountRightModel> GetAccountRightByIDFromDataSource_Async(int searchAccountRightID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return await Task.FromResult<AccountRightModel>(sqlContext.AccountRight.Find(searchAccountRightID)).ConfigureAwait(false);
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

        public AccountRightModel UpdateAccountRightToDataSource(AccountRightModel updateAccountRight)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountRightModel existingAccountRight = sqlContext.AccountRight.Find(updateAccountRight.ID);
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

        public async Task<AccountRightModel> UpdateAccountRightToDataSource_Async(AccountRightModel updateAccountRight)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountRightModel existingAccountRight = sqlContext.AccountRight.Find(updateAccountRight.ID);
                        if (existingAccountRight != null)
                        {
                            existingAccountRight.ID = updateAccountRight.ID;
                            existingAccountRight.RightID = updateAccountRight.RightID;
                            existingAccountRight.Account = updateAccountRight.Account;
                            existingAccountRight.LastUpdate = updateAccountRight.LastUpdate;
                            sqlContext.SaveChanges();
                            return await Task.FromResult(existingAccountRight).ConfigureAwait(false);
                        }
                        else
                            return await AddAccountRightToDataSource_Async(updateAccountRight).ConfigureAwait(false);
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
