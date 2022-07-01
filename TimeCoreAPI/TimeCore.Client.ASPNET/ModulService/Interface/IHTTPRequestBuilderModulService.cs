using System.Net.Http;

namespace TimeCore.Client.ASPNET.ModulService
{
    public interface IHTTPRequestBuilderModulService
    {
        HttpClient CreateHttpClient(string selectedhostURL, string selectedUserGUID);
    }
}