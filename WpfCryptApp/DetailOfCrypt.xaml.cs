using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string SearchString { get; set; }

        public DetailOfCrypt(string str)
        {
            InitializeComponent();

            SearchString = str;

            Test.Text = CryptViewModel.SearchCrypt(SearchString).Name;
        }

        private void ButtonUpdate(object sender, RoutedEventArgs e)
        {
            CryptViewModel cryptViewModel = new CryptViewModel();

            DetailOfCrypt detailOfCrypt = new DetailOfCrypt(SearchString);
            detailOfCrypt.Show();

            this.Close();
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}