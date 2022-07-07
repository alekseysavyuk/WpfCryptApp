using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WpfCryptApp
{
    public class CryptViewModel : HttpConnectionHelper, INotifyPropertyChanged
    {
        /// <summary>
        /// Доступ до контексту даних та коллекція об'єктів даних
        /// </summary>
        private CryptInfo? selectedCryptInfo;
        public static ObservableCollection<CryptInfo>? CryptInfoList { get; set; }
        public CryptInfo? SelectedCryptInfo
        {
            get { return selectedCryptInfo; }
            set { selectedCryptInfo = value; OnPropertyChanged("SelectedCryptInfo"); }
        }
        
        public CryptViewModel() => GetAllColletionCrypt();

        /// <summary>
        /// Пошук криптовалюти по назві, символу аб айді в коллекції
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static CryptInfo SearchCrypt(string str)
        {
            foreach (var search in CryptInfoList)
            {
                if (str.ToLower() == search?.Name?.ToLower() || str.ToLower() == search?.Symbol?.ToLower() || str.ToLower() == search?.Id?.ToLower())
                    return search;
            }

            return null;
        }
        
        /// <summary>
        /// Отримання даних одної криптовалюти з Api
        /// </summary>
        /// <param name="crypt_name"></param>
        /// <returns></returns>
        public async Task<CryptInfo> GetOnlyCrypt(string crypt_name)
        {
            CryptInfo tempInfo = null;
            ApiAssetsId = crypt_name;

            try
            {
                dynamic? crypt = await GetStringFromApi(ApiAssetsId);

                if (crypt.data != null)
                {
                    tempInfo = new CryptInfo();
                    tempInfo.Id = crypt.id;
                    tempInfo.Name = crypt.name;
                    tempInfo.Symbol = crypt.symbol;
                    tempInfo.Price = crypt.priceUsd;
                    tempInfo.Change_24h = crypt.changePercent24Hr;
                    tempInfo.Updated_DateTime = DateTime.UtcNow;
                }

                else throw new Exception("Value Crypt is Empty!!!");
            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }

            return tempInfo;
        }
        
        /// <summary>
        /// Отримання коллекції криптовалют з Api
        /// </summary>
        public async void GetAllColletionCrypt()
        {
            try
            {
                dynamic? crypt = await GetStringFromApi(ApiAssets);

                CryptInfo tempInfo;
                CryptInfoList = new ObservableCollection<CryptInfo>();

                foreach (var crt in crypt.data)
                {
                    if (crypt.data != null)
                    {
                        tempInfo = new CryptInfo();
                        tempInfo.Id = crt.id;
                        tempInfo.Name = crt.name;
                        tempInfo.Symbol = crt.symbol;
                        tempInfo.Price = crt.priceUsd;
                        CryptInfoList.Add(tempInfo);
                    }

                    else break;
                }
            }

            catch (Exception ex) { Console.WriteLine($"\nError!!!\n" + ex.Message); }
        }

        /// <summary>
        /// Виклик події при додаванні даних
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}