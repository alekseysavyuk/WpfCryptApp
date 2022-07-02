using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfCryptApp
{
    public class HttpConnectionHelper
    {
        private static string Address { get; set; } = "https://api.coincap.io/v2/assets";

        private SocketsHttpHandler sockeyHandler = new SocketsHttpHandler
        {
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2)
        };

        protected async Task<StringBuilder> GetStringFromApi()
        {
            StringBuilder body = null;

            try
            {
                var client = new HttpClient(sockeyHandler);

                var request = new HttpRequestMessage(HttpMethod.Get, Address);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                body = new StringBuilder(await response.Content.ReadAsStringAsync());
            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }

            return body;
        }
    }
}