using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfCryptApp
{
    /// <summary>
    /// Логика взаимодействия для DetailOfCrypt.xaml
    /// </summary>
    public partial class DetailOfCrypt : Window
    {
        private string? SearchString;

        private CryptViewModel cryptView;

        public DetailOfCrypt(string searchName)
        {
            CryptInfo cryptInfo = CryptViewModel.SearchCrypt(searchName);

            MarketsViewModel marketsView = new MarketsViewModel(cryptInfo.Name);
            //LoadWindow(cryptInfo);

            InitializeComponent();

            //LoadWindow(cryptInfo);
            //CryptInfo cryptInfo = CryptViewModel.SearchCrypt(searchName);

            //LoadWindow(cryptInfo);

            SearchString = cryptInfo.Id;
            Test.Text = cryptInfo.Name;

            listBoxMarkets.ItemsSource = MarketsViewModel.CryptInfoInMarketList?.Take(10);
            DataContext = marketsView;
        }

        public DetailOfCrypt(CryptInfo cryptInfo)
        {
            MarketsViewModel marketsView = new MarketsViewModel(cryptInfo.Name);

            InitializeComponent();

            SearchString = cryptInfo.Id;
            Test.Text = cryptInfo.Name;

            //LoadWindow(cryptInfo);
            listBoxMarkets.ItemsSource = MarketsViewModel.CryptInfoInMarketList?.Take(10);
            DataContext = marketsView;
        }

        private async void LoadWindow(CryptInfo crypt)
        {
            //MainWindow.cryptViewModel = new CryptViewModel(crypt.Name);

            //Thread.Sleep(800);

            SearchString = crypt.Id;
            Test.Text = crypt.Name;

            //listBoxTop.ItemsSource = CryptViewModel.CryptInfoInMarketList?.Take(10);
            //DataContext = MainWindow.cryptViewModel;
        }

        private async void ButtonUpdate(object sender, RoutedEventArgs e)
        {
            var cryptInfo = await MainWindow.cryptViewModel.GetOnlyCrypt(SearchString.ToString());

            DetailOfCrypt detailOfCrypt = new DetailOfCrypt(cryptInfo);
            detailOfCrypt.Show();

            this.Close();
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}