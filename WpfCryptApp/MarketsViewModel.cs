using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WpfCryptApp
{
    public class MarketsViewModel : HttpConnectionHelper, INotifyPropertyChanged
    {
        /// <summary>
        /// Доступ до контексту даних та коллекції об'єктів даних
        /// </summary>
        private CryptInfo? selectedCryptInfoInMarket;
        public static ObservableCollection<CryptInfo>? CryptInfoInMarketList { get; set; } = new ObservableCollection<CryptInfo>();
        public CryptInfo? SelectedCryptInfoInMarket
        {
            get { return selectedCryptInfoInMarket; }
            set { selectedCryptInfoInMarket = value; OnPropertyChanged("SelectedCryptInfoInMarket"); }
        }

        public MarketsViewModel(string i) => GetCryptsInMarkets(i);

        /// <summary>
        /// Отримання коллекції даних про ринки криптовалют
        /// </summary>
        /// <param name="cryptName"></param>
        /// <returns></returns>
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
                    if (crypt != null)
                    {
                        tempInfo = new CryptInfo();
                        tempInfo.Market = crt.exchangeId;
                        tempInfo.Name = crt.baseId;
                        tempInfo.Symbol = crt.baseSymbol;
                        tempInfo.Price = crt.priceUsd;
                        //tempInfo.Supply = crt.supply;
                        //tempInfo.Change_24h = crt.changePercent24Hr;
                        //tempInfo.Updated_DateTime = DateTime.UtcNow;
                        CryptInfoInMarketList.Add(tempInfo);
                    }

                    else break;
                }
            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }
        }

        /// <summary>
        /// Виклик події додавання даних
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}