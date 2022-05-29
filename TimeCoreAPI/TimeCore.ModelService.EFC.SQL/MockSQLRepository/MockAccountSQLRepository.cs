using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCore.ErrorHandler;

namespace TimeCore.ModelService.EFC.SQL
{
    public class MockAccountSQLRepository : IAccountSQLRepository
    {
        public MockAccountSQLRepository()
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
                        return sqlContext.Account.Where(s => s.Username == accountUserName && 
                        s.Password == accountPassword &&
                        s.Workshop.ID == workshopID).FirstOrDefault<AccountModel>();
                    }
                    else
                    {
                        ErrorHandlerLog.WriteError("MockAccountSQLRepository.GetAccountByCredentialsFromDataSource(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"MockAccountSQLRepository.GetAccountByIDFromDataSource(): {ex.Message}");
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
                        ErrorHandlerLog.WriteError("MockAccountSQLRepository.GetAccountByCredentialsFromDataSource_Async(): Keine Verbindung zur Datenbank möglich");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerLog.WriteError($"MockAccountSQLRepository.GetAccountByIDFromDataSource_Async(): {ex.Message}");
                return null;
            }
        }

    }
}
