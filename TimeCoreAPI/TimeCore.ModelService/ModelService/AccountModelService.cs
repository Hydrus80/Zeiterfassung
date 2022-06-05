using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeCore.ModelService.EFC.SQL;
using static TimeCore.ModelService.SupportedDatabaseType;

namespace TimeCore.ModelService
{
    public class AccountModelService : IAccountModelService
    {
        public eDatabaseType modelDatabaseType;
        public IAccountSQLRepository accountSQLRepository;

        public AccountModelService(eDatabaseType selectedDatabaseType, 
            IAccountSQLRepository selectAccountSQLRepository)
        {
            modelDatabaseType = selectedDatabaseType;
            accountSQLRepository = selectAccountSQLRepository;
        }

        public AccountModelService()
        {
            modelDatabaseType = eDatabaseType.SQL; ;
            accountSQLRepository = new AccountSQLRepository();
        }

        public AccountModel Authenticate(string accountUserName, string accountPassword)
        {
            //Daten holen
            AccountModel authAccount = accountSQLRepository.GetAccountByCredentialsFromDataSource(accountUserName, accountPassword);

            //Wenn gefunden, ohne Passwort zurück geben
            if (authAccount.ID > 0)
                authAccount.WithoutPassword();

            //zurück
           return authAccount;
        }

        public async Task<AccountModel> AuthenticateAsync(string accountUserName, string accountPassword)
        {
            //Daten holen
            AccountModel authAccount = await accountSQLRepository.GetAccountByCredentialsFromDataSourceAsync(accountUserName, accountPassword).ConfigureAwait(false);

            //Wenn gefunden, ohne Passwort zurück geben
            if (authAccount.ID > 0)
                authAccount.WithoutPassword();

            //zurück
            return authAccount;
        }

        public AccountModel GetAccountByGUID(string accountGUID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return accountSQLRepository.GetAccountByGUIDFromDataSource(accountGUID);
            }
            else
                return null;
        }

        public async Task<AccountModel> GetAccountByGUIDAsync(string accountGUID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await accountSQLRepository.GetAccountByGUIDFromDataSourceAsync(accountGUID).ConfigureAwait(false);
            }
            else
                return null;
        }
    }
}
