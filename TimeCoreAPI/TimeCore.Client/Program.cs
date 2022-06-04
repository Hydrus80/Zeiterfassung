using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TimeCoreClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            using (HttpClient client = new HttpClient())
            {
                var respone = await client.GetAsync("http://localhost:5000/api/TimeCore/SQL/Login/bach/bach/1");
                respone.EnsureSuccessStatusCode();
                if (respone.IsSuccessStatusCode)
                {
                    string APIresponse = await respone.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Console.WriteLine($"Response: {APIresponse}");
                }
            }
        }
    }
}
