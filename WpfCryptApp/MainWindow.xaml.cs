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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCryptApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static CryptViewModel? cryptViewModel;

        public MainWindow()
        {
            cryptViewModel = new CryptViewModel();

            InitializeComponent();

            

            DataContext = cryptViewModel;
        }

        public void ThemeChange(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;

            string? style = comboBoxItem.Content.ToString();
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            ResourceDictionary? resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        private void ButtonUpdate(object sender, RoutedEventArgs e)
        {   
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void ActiveSearch(object sender, RoutedEventArgs e)
        {
            string? crypt = CryptViewModel.SearchCrypt(nameSearch.Text).Name;

            ///proverka
            DetailOfCrypt detailOfCrypt = new DetailOfCrypt(crypt);
            detailOfCrypt.Show();
        }
        
        private void ButtonMore(object sender, RoutedEventArgs e)
        {
            AllCryptWindow allCryptWindow = new AllCryptWindow();
            allCryptWindow.Show();
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}