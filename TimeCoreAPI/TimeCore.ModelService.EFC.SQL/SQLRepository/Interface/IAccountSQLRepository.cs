using System.Threading.Tasks;
using Model;

namespace TimeCore.ModelService.EFC.SQL
{
    public interface IAccountSQLRepository
    {
        AccountModel GetAccountByCredentialsFromDataSource(string accountUserName, string accountPassword, int workshopID);
        Task<AccountModel> GetAccountByCredentialsFromDataSource_Async(string accountUserName, string accountPassword, int workshopID);
    }
}
