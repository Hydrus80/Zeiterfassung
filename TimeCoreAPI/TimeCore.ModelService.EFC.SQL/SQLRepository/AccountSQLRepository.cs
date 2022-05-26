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
        //Context
        private readonly SQLContext sqlContext;

        public AccountSQLRepository(SQLContext selSQLContext)
        {
            sqlContext = selSQLContext;
        }

        public IAccountModel AddAccountToDataSource(IAccountModel newAccount)
        {
            try
            {
                AccountModel insertEntry = new AccountModel()
                {
                    Username = newAccount.Username,
                    Password = newAccount.Password,
                    Workshop = newAccount.Workshop,
                    LastUpdate = newAccount.LastUpdate,
                };
                sqlContext.Account.Add(insertEntry);
                if (sqlContext.Database.CanConnect())
                {
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
                AccountModel insertEntry = new AccountModel()
                {
                    Username = newAccount.Username,
                    Password = newAccount.Password,
                    Workshop = newAccount.Workshop,
                    LastUpdate = newAccount.LastUpdate,
                };
                sqlContext.Account.Add(insertEntry);
                if (sqlContext.Database.CanConnect())
                {
                    //m_partcontext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT PartStorage ON"); //ExecuteSqlCommand
                    sqlContext.SaveChanges();
                    return Task.FromResult<IAccountModel>(insertEntry);
                }
                else
                {
                    ErrorHandlerLog.WriteError("AccountSQLRepository.AddAccountToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                    return null;
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
                if (sqlContext.Database.CanConnect())
                {
                    return Task.FromResult<IAccountModel>(sqlContext.Account.Find(searchAccountID));
                }
                else
                {
                    ErrorHandlerLog.WriteError("AccountSQLRepository.GetAccountByIDFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                    return null;
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
                    ErrorHandlerLog.WriteError("AccountSQLRepository.UpdateAccountToDataSource(): Keine Verbindung zur Datenbank möglich");
                    return null;
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
                    ErrorHandlerLog.WriteError("AccountSQLRepository.UpdateAccountToDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                    return null;
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
