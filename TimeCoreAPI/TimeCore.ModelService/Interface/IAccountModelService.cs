using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeCore.ModelService
{
    public interface IAccountModelService
    {
        AccountModel GetAccountByCredentials(string accountUserName, string accountPassword, int workshopID);
        Task<AccountModel> GetAccountByCredentials_Async(string accountUserName, string accountPassword, int workshopID);
    }
}