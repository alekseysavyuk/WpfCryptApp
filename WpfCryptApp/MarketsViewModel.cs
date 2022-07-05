using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfCryptApp
{
    public class MarketsViewModel : HttpConnectionHelper, INotifyPropertyChanged
    {
        private CryptInfo? selectedCryptInfoInMarkets;
        public static ObservableCollection<CryptInfo>? CryptInfoInMarketList { get; set; }
        public CryptInfo? SelectedCryptInfoInMarket
        {
            get { return selectedCryptInfoInMarkets; }
            set { selectedCryptInfoInMarkets = value; OnPropertyChanged("SelectedCryptInfoInMarkets"); }
        }

        public MarketsViewModel(string i) => GetCryptsInMarkets(i);

        public async Task GetCryptsInMarkets(string cryptName)
        {
            try
            {
                ApiAssetsMarkets = cryptName;

                dynamic? crypt = await GetStringFromApi(ApiAssetsMarkets);

                CryptInfo tempInfo;
                CryptInfoInMarketList = new ObservableCollection<CryptInfo>();

                foreach (var crt in crypt.data)
                {
                    if (crypt.data != null)
                    {
                        tempInfo = new CryptInfo();
                        tempInfo.Market = crt.exchangeId;
                        tempInfo.Name = crt.baseId;
                        tempInfo.Symbol = crt.baseSymbol;
                        tempInfo.Price = crt.priceUsd;
                        //tempInfo.Change_24h = crt.changePercent24Hr;
                        tempInfo.Updated_DateTime = DateTime.UtcNow;
                        CryptInfoInMarketList.Add(tempInfo);
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
