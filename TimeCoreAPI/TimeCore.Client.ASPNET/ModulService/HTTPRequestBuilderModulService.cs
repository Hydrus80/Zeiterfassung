using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TimeCore.Client.ASPNET.ModulService
{
    public class HTTPRequestBuilderModulService : IHTTPRequestBuilderModulService
    {
        public HttpClient CreateHttpClient(string selectedhostURL, string selectedUserGUID)
        {
            // Update port # in the following line.
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{selectedhostURL}/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if(!string.IsNullOrEmpty(selectedUserGUID))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", selectedUserGUID);
            return client;
        }
    }
}
