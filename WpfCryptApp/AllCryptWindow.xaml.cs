using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WpfCryptApp
{
    /// <summary>
    /// Логика взаимодействия для AllCryptWindow.xaml
    /// </summary>
    public partial class AllCryptWindow : Window
    {
        public AllCryptWindow()
        {
            InitializeComponent();

            LoadCryptInfoList(CryptViewModel.CryptInfoList.ToList());
        }

        /// <summary>
        /// Оновлення вікна та даних з Api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdate(object sender, RoutedEventArgs e)
        {
            MainWindow.cryptViewModel = new CryptViewModel();

            AllCryptWindow allCryptWindow = new AllCryptWindow();
            allCryptWindow.Show();

            this.Close();
        }

        /// <summary>
        /// Активація пошуку криптовалюти та перехід у вікно криптовалюти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveSearch(object sender, RoutedEventArgs e)
        {
            CryptInfo? nameSearch = AllCryptListView.SelectedItem as CryptInfo;

            if (nameSearch == null)
                nameSearch = new CryptInfo() { Name = "I didn`t seached crypt!!!" };

            DetailOfCrypt detailOfCrypt = new DetailOfCrypt(nameSearch.Name);
            detailOfCrypt.Show();
        }

        /// <summary>
        /// Виведення списку знайдених криптовалют
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveFilter(object sender, RoutedEventArgs e)
        {
            var searchList = new List<CryptInfo>();

            foreach (var search in CryptViewModel.CryptInfoList)
            {
                if (nameSearch.Text.ToLower() == search?.Name?.ToLower() || nameSearch.Text.ToLower() == search?.Symbol?.ToLower() || nameSearch.Text.ToLower() == search?.Id?.ToLower())
                    searchList.Add(search);
            }

            LoadCryptInfoList(searchList);
        }

        /// <summary>
        /// Завантаження даних до форми
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