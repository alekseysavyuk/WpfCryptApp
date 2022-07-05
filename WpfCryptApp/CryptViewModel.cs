using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfCryptApp
{
    public class CryptViewModel : HttpConnectionHelper, INotifyPropertyChanged
    {
        private CryptInfo? selectedCryptInfo;
        public static ObservableCollection<CryptInfo>? CryptInfoList { get; set; }
        public CryptInfo? SelectedCryptInfo
        {
            get { return selectedCryptInfo; }
            set { selectedCryptInfo = value; OnPropertyChanged("SelectedCryptInfo"); }
        }
        /*
        private CryptInfo? selectedCryptInfoInMarkets;
        public static ObservableCollection<CryptInfo>? CryptInfoInMarketList { get; set; }
        public CryptInfo? SelectedCryptInfoInMarket
        {
            get { return selectedCryptInfoInMarkets; }
            set { selectedCryptInfoInMarkets = value; OnPropertyChanged("SelectedCryptInfoInMarkets"); }
        }
        */
        public CryptViewModel() => GetAllColletionCrypt();
        //public CryptViewModel(string i) => GetCryptsInMarkets(i);

        public static CryptInfo SearchCrypt(string str)
        {
            foreach (var search in CryptInfoList)
            {
                if (str.ToLower() == search?.Name?.ToLower() || str.ToLower() == search?.Symbol?.ToLower() || str.ToLower() == search?.Id?.ToLower())
                    return search;
            }

            return null;
        }

        public async Task<decimal> GetOnlyCryptToRate(string crypt_name)
        {
            decimal rate = default;

            try
            {
                dynamic? crypt = await GetStringFromApi(ApiAssetsId = crypt_name);

                rate = crypt.data.rateUsd;
            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }

            return rate;
        }

        public async Task<CryptInfo> GetOnlyCrypt(string crypt_name)
        {
            CryptInfo tempInfo = null;
            ApiAssetsId = crypt_name;

            try
            {
                dynamic? crypt = await GetStringFromApi(ApiAssetsId);

                if (crypt != null)
                {
                    tempInfo = new CryptInfo();
                    tempInfo.Id = crypt.data.id;
                    tempInfo.Name = crypt.data.name;
                    tempInfo.Symbol = crypt.data.symbol;
                    tempInfo.Price = crypt.data.priceUsd;
                    tempInfo.Change_24h = crypt.data.changePercent24Hr;
                    tempInfo.Updated_DateTime = DateTime.UtcNow;
                }

                else throw new Exception("Value Crypt is Empty!!!");
            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }

            return tempInfo;
        }
        /*
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
        */
        public async void GetAllColletionCrypt()
        {
            try
            {
                dynamic? crypt = await GetStringFromApi(ApiAssets);

                //int run = 0;
                CryptInfo tempInfo;
                CryptInfoList = new ObservableCollection<CryptInfo>();

                foreach (var crt in crypt.data)
                {
                    //if (run == 10) break;

                    if (crypt.data != null)
                    {
                        tempInfo = new CryptInfo();
                        tempInfo.Id = crt.id;
                        tempInfo.Name = crt.name;
                        tempInfo.Symbol = crt.symbol;
                        tempInfo.Price = crt.priceUsd;
                        tempInfo.Change_24h = crt.changePercent24Hr;
                        tempInfo.Updated_DateTime = DateTime.UtcNow;
                        CryptInfoList.Add(tempInfo);

                        //run++;
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