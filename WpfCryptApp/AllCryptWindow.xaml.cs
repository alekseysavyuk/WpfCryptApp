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
    /// Логика взаимодействия для AllCryptWindow.xaml
    /// </summary>
    public partial class AllCryptWindow : Window
    {
        public AllCryptWindow()
        {
            InitializeComponent();

            LoadCryptInfoList(CryptViewModel.CryptInfoList?.ToList());
        }

        private void ActiveSearch(object sender, RoutedEventArgs e)
        {
            var name = AllCryptListView.SelectedItem as CryptInfo;
            
            DetailOfCrypt detailOfCrypt = new DetailOfCrypt(name);
            detailOfCrypt.Show();
        }

        private void ActiveFilter(object sender, RoutedEventArgs e)
        {
            var searchList = new List<CryptInfo>();

            foreach (var search in CryptViewModel.CryptInfoList)
            {
                if (nameSearch.Text.ToLower() == search?.Name?.ToLower() || nameSearch.Text.ToLower() == search?.Symbol?.ToLower())
                    searchList.Add(search);
            }

            LoadCryptInfoList(searchList);
        }

        private void LoadCryptInfoList(List<CryptInfo> cryptList)
        {
            AllCryptListView.Items.Clear();

            if (cryptList.Count == 0)
                AllCryptListView.Items.Add(new CryptInfo() { Name = "Not loaded this crypt" });

            foreach (var crypt in cryptList)
            {
                AllCryptListView.Items.Add(crypt);
            }
        }
    }
}
