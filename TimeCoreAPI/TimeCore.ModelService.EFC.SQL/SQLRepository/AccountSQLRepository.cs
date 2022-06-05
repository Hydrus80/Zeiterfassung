using Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class AccountSQLRepository : IAccountSQLRepository
    {
        public AccountSQLRepository()
        { }

        public AccountModel GetAccountByCredentialsFromDataSource(string accountUserName, string accountPassword)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountModel returnValue = sqlContext.Account.Where(s => s.Username == accountUserName &&
                        s.Password == accountPassword).FirstOrDefault<AccountModel>();

                        //Add Workshop
                        returnValue.Workshop = sqlContext.Workshop.Find(returnValue.WorkshopID);
                        return returnValue;
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

        public async Task<AccountModel> GetAccountByCredentialsFromDataSourceAsync(string accountUserName, string accountPassword)
        {
            try
            {
                return await Task.FromResult<AccountModel>(GetAccountByCredentialsFromDataSource(accountUserName, accountPassword)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.GetAccountByIDFromDataSourceAsync(): {ex.Message}");
                return null;
            }
        }

        public AccountModel GetAccountByGUIDFromDataSource(string accountGUID)
        {
            try
            {
                //Context setzen
                using (SQLContext sqlContext = new SQLContext())
                {
                    //Existiert Datenbank und ist der Zugriff gewährt?
                    if (sqlContext.Database.CanConnect())
                    {
                        AccountModel returnValue = sqlContext.Account.Where(s => s.GUID == accountGUID).FirstOrDefault<AccountModel>();

                        //Add Workshop
                        returnValue.Workshop = sqlContext.Workshop.Find(returnValue.WorkshopID);
                        return returnValue;
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("AccountSQLRepository.GetAccountByGUIDFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.GetAccountByGUIDFromDataSource(): {ex.Message}");
                return null;
            }
        }

        public async Task<AccountModel> GetAccountByGUIDFromDataSourceAsync(string accountGUID)
        {
            try
            {
                return await Task.FromResult<AccountModel>(GetAccountByGUIDFromDataSource(accountGUID)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.GetAccountByIDFromDataSourceAsync(): {ex.Message}");
                return null;
            }
        }


    }
}
