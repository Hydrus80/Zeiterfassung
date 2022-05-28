using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class AccountSQLRepository : IAccountSQLRepository
    {
        public AccountSQLRepository()
        { }

        public AccountModel AddAccountToDataSource(AccountModel newAccount)
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
                        ErrorHandlerLog.WriteError("AccountSQLRepository.AddAccountToDataSource(): Keine Verbindung zur Datenbank möglich");
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

        public async Task<AccountModel> AddAccountToDataSource_Async(AccountModel newAccount)
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
                        return await Task.FromResult<AccountModel>(insertEntry).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("AccountSQLRepository.AddAccountToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
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

        public AccountModel GetAccountByCredentialsFromDataSource(string accountUserName, string accountPassword, int workshopID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return sqlContext.Account.Where(s => s.Username == accountUserName && 
                        s.Password == accountPassword &&
                        s.Workshop.ID == workshopID).FirstOrDefault<AccountModel>();
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("AccountSQLRepository.GetAccountByCredentialsFromDataSource(): Keine Verbindung zur Datenbank möglich");
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

        public async Task<AccountModel> GetAccountByCredentialsFromDataSource_Async(string accountUserName, string accountPassword, int workshopID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return await Task.FromResult<AccountModel>(sqlContext.Account.Where(s => s.Username == accountUserName &&
                        s.Password == accountPassword &&
                        s.Workshop.ID == workshopID).FirstOrDefault<AccountModel>()).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("AccountSQLRepository.GetAccountByCredentialsFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
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

        public AccountModel GetAccountByIDFromDataSource(int searchAccountID)
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
                        ErrorHandlerLog.WriteError("AccountSQLRepository.GetAccountByIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
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

        public async Task<AccountModel> GetAccountByIDFromDataSource_Async(int searchAccountID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        return await sqlContext.Account.FindAsync(searchAccountID).ConfigureAwait(false);
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("AccountSQLRepository.GetAccountByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
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

        public AccountModel UpdateAccountToDataSource(AccountModel updateAccount)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountModel existingAccount = sqlContext.Account.Find(updateAccount.ID);
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
                        ErrorHandlerLog.WriteError("AccountSQLRepository.UpdateAccountToDataSource(): Keine Verbindung zur Datenbank möglich");
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

        public async Task<AccountModel> UpdateAccountToDataSource_Async(AccountModel updateAccount)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountModel existingAccount = sqlContext.Account.Find(updateAccount.ID);
                        if (existingAccount != null)
                        {
                            existingAccount.ID = updateAccount.ID;
                            existingAccount.Username = updateAccount.Username;
                            existingAccount.Password = updateAccount.Password;
                            existingAccount.Workshop = updateAccount.Workshop;
                            existingAccount.LastUpdate = updateAccount.LastUpdate;
                            sqlContext.SaveChanges();
                            return await Task.FromResult(existingAccount).ConfigureAwait(false);
                        }
                        else
                            return await AddAccountToDataSource_Async(updateAccount).ConfigureAwait(false); ;
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("AccountSQLRepository.UpdateAccountToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
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
