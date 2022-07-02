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
    public class CryptViewModel : HttpConnectionHelper, INotifyPropertyChanged
    {
        private CryptInfo? selectedCryptInfo;
        public ObservableCollection<CryptInfo>? CryptInfoList { get; set; }
        public CryptInfo? SelectedCryptInfo
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

                if (body == null) throw new Exception("String from API is Empty!");

                dynamic? crypt = JsonConvert.DeserializeObject(body.ToString());

                int run = 0;
                CryptInfo tempInfo;
                CryptInfoList = new ObservableCollection<CryptInfo>();

                foreach (var crt in crypt.data)
                {
                    if (run == 10) break;

                    if (crypt.data != null)
                    {
                        tempInfo = new CryptInfo();
                        tempInfo.Name = crt.name;
                        tempInfo.Symbol = crt.symbol;
                        tempInfo.Price = crt.priceUsd;
                        tempInfo.Change_24h = crt.changePercent24Hr;
                        tempInfo.Updated_DateTime = DateTime.UtcNow;
                        CryptInfoList.Add(tempInfo);

                        run++;
                    }

                    else break;
                }

            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
