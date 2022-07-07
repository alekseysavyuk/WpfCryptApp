using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WpfCryptApp
{
    public class HttpConnectionHelper
    {
        /// <summary>
        /// Api отрмання всіх монет
        /// </summary>
        protected static string ApiAssets { get; set; } = "https://api.coincap.io/v2/assets";

        /// <summary>
        /// Api отримання монети по айді
        /// </summary>
        private static string apiAssetsId;
        public static string ApiAssetsId
        {
            get { return apiAssetsId; }
            set { apiAssetsId = $"https://api.coincap.io/v2/assets/{value.ToLower()}"; }
        }
        
        /// <summary>
        /// Api отримання всіх крипторинків
        /// </summary>
        private static string apiAssetsMarkets;
        public static string ApiAssetsMarkets
        {
            get { return apiAssetsMarkets; }
            set { apiAssetsMarkets = $"https://api.coincap.io/v2/assets/{value.ToLower()}/markets"; }
        }

        /// <summary>
        /// Переривання з'єднання при довготривалому очікуванні (2 хвилини)
        /// </summary>
        private SocketsHttpHandler sockeyHandler = new SocketsHttpHandler
        {
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2)
        };

        /// <summary>
        /// З'днання для отримання даних з Api
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        protected async Task<dynamic> GetStringFromApi(string api = "")
        {
            dynamic? crypt = null;

            try
            {
                var client = new HttpClient(sockeyHandler);

                var request = new HttpRequestMessage(HttpMethod.Get, api);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string body = await response.Content.ReadAsStringAsync();

                if (body == null) throw new Exception("String from API is Empty!");

                crypt = JsonConvert.DeserializeObject(body);
            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }

            return crypt;
        }
    }
}