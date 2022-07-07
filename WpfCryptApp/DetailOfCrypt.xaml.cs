using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfCryptApp
{
    /// <summary>
    /// Логика взаимодействия для DetailOfCrypt.xaml
    /// </summary>
    public partial class DetailOfCrypt : Window
    {
        private static CryptInfo cryptInfo;
        private string? SearchString;
        private MarketsViewModel marketsView;

        public DetailOfCrypt(string searchName)
        {
            cryptInfo = CryptViewModel.SearchCrypt(searchName);

            marketsView = new MarketsViewModel(cryptInfo.Name);

            InitializeComponent();

            LoadData();

            LoadCryptInfoList(MarketsViewModel.CryptInfoInMarketList?.ToList());
        }

        public DetailOfCrypt(CryptInfo cryptInfoPar)
        {
            marketsView = new MarketsViewModel(cryptInfo.Name);

            InitializeComponent();

            LoadData();

            LoadCryptInfoList(MarketsViewModel.CryptInfoInMarketList?.ToList());
        }

        /// <summary>
        /// Завантаження даних про обрану криптовалюту
        /// </summary>
        private void LoadData()
        {
            textName.Text = cryptInfo.Name;
            textSymbol.Text = cryptInfo.Symbol;
            textPrice.Text = cryptInfo.Price.ToString();
        }

        /// <summary>
        /// Пошук введеного ринку криптовалюти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveFilter(object sender, RoutedEventArgs e)
        {
            var searchList = new List<CryptInfo>();

            foreach (var search in MarketsViewModel.CryptInfoInMarketList)
            {
                if (nameSearch.Text.ToLower() == search?.Market?.ToLower())
                    searchList.Add(search);
            }

            LoadCryptInfoList(searchList);
        }

        /// <summary>
        /// Завантадення знайдених згідно з пошуком ринків
        /// </summary>
        /// <param name="cryptList"></param>
        private void LoadCryptInfoList(List<CryptInfo> cryptList)
        {
            AllCryptListView.Items.Clear();

            if (cryptList.Count == 0)
                AllCryptListView.Items.Add(new CryptInfo() { Name = "Not loaded that crypt" });

            foreach (var crypt in cryptList)
                AllCryptListView.Items.Add(crypt);
        }

        /// <summary>
        /// Оновлення даних з Api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonUpdate(object sender, RoutedEventArgs e)
        {
            var crypt = await MainWindow.cryptViewModel.GetOnlyCrypt(cryptInfo.Name.ToString());

            DetailOfCrypt detailOfCrypt = new DetailOfCrypt(crypt);
            detailOfCrypt.Show();

            this.Close();
        }

        /// <summary>
        /// Відкриття вікно для переводу в іншу криптовалюту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonExchange(object sender, RoutedEventArgs e)
        {
            ExchangeCrypt exchange = new ExchangeCrypt(cryptInfo);
            exchange.Show();
        }

        /// <summary>
        /// Закриття вікна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}