using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfCryptApp
{
    public class CryptViewModel : INotifyPropertyChanged
    {
        public static string Address { get; set; } = "https://api.coincap.io/v2/assets";

        private CryptInfo selectedCryptInfo;
        public ObservableCollection<CryptInfo> CryptInfoList { get; set; }
        public CryptInfo SelectedCryptInfo
        {
            get { return selectedCryptInfo; }
            set { selectedCryptInfo = value; OnPropertyChanged("SelectedCryptInfo"); }
        }

        public CryptViewModel() => GetColletionCrypt();

        public async void GetColletionCrypt()
        {
            try
            {
                StringBuilder body = await GetStringFromApi();

                dynamic crypt = JsonConvert.DeserializeObject(body.ToString());

                int run = 0;
                CryptInfo tempInfo;
                CryptInfoList = new ObservableCollection<CryptInfo>();

                foreach (var crt in crypt.data)
                {
                    if (run == 10) break;

                    tempInfo = new CryptInfo();
                    tempInfo.Name = crt.name;
                    tempInfo.Symbol = crt.symbol;
                    tempInfo.Price = crt.priceUsd;
                    tempInfo.Change_24h = crt.changePercent24Hr;
                    tempInfo.Updated_DateTime = DateTime.UtcNow;
                    CryptInfoList.Add(tempInfo);

                    run++;
                }

            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }
        }

        private async Task<StringBuilder> GetStringFromApi()
        {
            StringBuilder body = null;

            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, Address);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                body = new StringBuilder(await response.Content.ReadAsStringAsync());
            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }

            return body;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
