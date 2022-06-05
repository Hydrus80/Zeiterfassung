using Model;
using System.Threading.Tasks;

namespace TimeCore.ModelService
{
    public interface IAccountModelService
    {
        AccountModel Authenticate(string accountUserName, string accountPassword);
        Task<AccountModel> AuthenticateAsync(string accountUserName, string accountPassword);
        AccountModel GetAccountByGUID(string accountGUID);
        Task<AccountModel> GetAccountByGUIDAsync(string accountGUID);
    }
}