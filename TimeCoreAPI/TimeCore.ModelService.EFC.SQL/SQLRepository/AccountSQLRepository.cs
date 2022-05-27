using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class AccountSQLRepository : IAccountSQLRepository
    {
        public AccountSQLRepository()
        { }

        public IAccountModel AddAccountToDataSource(IAccountModel newAccount)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountModel insertEntry = new AccountModel()
                        {
                            Username = newAccount.Username,
                            Password = newAccount.Password,
                            Workshop = newAccount.Workshop,
                            LastUpdate = newAccount.LastUpdate,
                        };
                        sqlContext.Account.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return insertEntry;
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.AddAccountToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.AddAccountToDataSource(): {ex.Message}");
                return null;
            }
        }

        public Task<IAccountModel> AddAccountToDataSource_Async(IAccountModel newAccount)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountModel insertEntry = new AccountModel()
                        {
                            Username = newAccount.Username,
                            Password = newAccount.Password,
                            Workshop = newAccount.Workshop,
                            LastUpdate = newAccount.LastUpdate,
                        };
                        sqlContext.Account.Add(insertEntry);

                        //Änderungen speichern
                        //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                        sqlContext.SaveChanges();
                        return Task.FromResult<IAccountModel>(insertEntry);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.AddAccountToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.AddAccountToDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public IAccountModel GetAccountByIDFromDataSource(int searchAccountID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.Account.Find(searchAccountID);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.GetAccountByIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.GetAccountByIDFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public Task<IAccountModel> GetAccountByIDFromDataSource_Async(int searchAccountID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return Task.FromResult<IAccountModel>(sqlContext.Account.Find(searchAccountID));
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.GetAccountByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.GetAccountByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }

        public IAccountModel UpdateAccountToDataSource(IAccountModel updateAccount)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        IAccountModel existingAccount = sqlContext.Account.Find(updateAccount.ID);
                        if (existingAccount != null)
                        {
                            existingAccount.ID = updateAccount.ID;
                            existingAccount.Username = updateAccount.Username;
                            existingAccount.Password = updateAccount.Password;
                            existingAccount.Workshop = updateAccount.Workshop;
                            existingAccount.LastUpdate = updateAccount.LastUpdate;
                            sqlContext.SaveChanges();
                            return existingAccount;
                        }
                        else
                            return AddAccountToDataSource(updateAccount);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.UpdateAccountToDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.UpdateAccountToDataSource(): {ex.Message}");
                return null;
            }
        }

        public Task<IAccountModel> UpdateAccountToDataSource_Async(IAccountModel updateAccount)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        IAccountModel existingAccount = sqlContext.Account.Find(updateAccount.ID);
                        if (existingAccount != null)
                        {
                            existingAccount.ID = updateAccount.ID;
                            existingAccount.Username = updateAccount.Username;
                            existingAccount.Password = updateAccount.Password;
                            existingAccount.Workshop = updateAccount.Workshop;
                            existingAccount.LastUpdate = updateAccount.LastUpdate;
                            sqlContext.SaveChanges();
                            return Task.FromResult(existingAccount);
                        }
                        else
                            return AddAccountToDataSource_Async(updateAccount);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("FirmSQLRepository.UpdateAccountToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.UpdateAccountToDataSource_Async(): {ex.Message}");
                return null;
            }
        }
    }
}
