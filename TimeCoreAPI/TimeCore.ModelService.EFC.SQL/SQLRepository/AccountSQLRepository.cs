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
                        AccountModel returnValue = sqlContext.Account.Where(s => s.Username == accountUserName &&
                        s.Password == accountPassword &&
                        s.Workshop.ID == workshopID).FirstOrDefault<AccountModel>();

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

        public async Task<AccountModel> GetAccountByCredentialsFromDataSource_Async(string accountUserName, string accountPassword, int workshopID)
        {
            try
            {
                return await Task.FromResult<AccountModel>(GetAccountByCredentialsFromDataSource(accountUserName, accountPassword, workshopID)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"AccountSQLRepository.GetAccountByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }

      
    }
}
