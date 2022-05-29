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

        public AccountModel GetAccountByCredentials(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return accountSQLRepository.GetAccountByCredentialsFromDataSource(accountUserName, accountPassword, workshopID);
            }
            else
                return null;
        }

        public async Task<AccountModel> GetAccountByCredentials_Async(string accountUserName, string accountPassword, int workshopID)
        {
            if (modelDatabaseType == eDatabaseType.SQL)
            {
                return await Task.FromResult(accountSQLRepository.GetAccountByCredentialsFromDataSource(accountUserName, accountPassword, workshopID)).ConfigureAwait(false);
            }
            else
                return null;
        }
    }
}
