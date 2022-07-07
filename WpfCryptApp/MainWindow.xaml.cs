using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfCryptApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Глобальна змінна для доступу до контексту даних
        /// </summary>
        public static CryptViewModel? cryptViewModel;

        public MainWindow()
        {
            cryptViewModel = new CryptViewModel();

            InitializeComponent();

            listBoxTop.ItemsSource = CryptViewModel.CryptInfoList?.Take(10);
            DataContext = cryptViewModel;
        }

        /// <summary>
        /// Метод змінення стилю додатку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThemeChange(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem comboBoxItem = (ComboBoxItem)comboBox.SelectedItem;

            string? style = comboBoxItem.Content.ToString();
            var uri = new Uri(style + ".xaml", UriKind.Relative);

            ResourceDictionary? resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        /// <summary>
        /// Метод оновлення даних отриманих з Api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdate(object sender, RoutedEventArgs e)
        {   
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        /// <summary>
        /// Активація пошуку криптовалюти по назві або символьному значенні
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveSearch(object sender, RoutedEventArgs e)
        {
            ///proverka
            DetailOfCrypt detailOfCrypt = new DetailOfCrypt(nameSearch.Text);
            detailOfCrypt.Show();
        }
        
        /// <summary>
        /// Перехід на нове вікно звсіма криптовалютами отриманими з Api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMore(object sender, RoutedEventArgs e)
        {
            AllCryptWindow allCryptWindow = new AllCryptWindow();
            allCryptWindow.Show();
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