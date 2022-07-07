using System.Windows;

namespace WpfCryptApp
{
    /// <summary>
    /// Логика взаимодействия для ExchangeCrypt.xaml
    /// </summary>
    public partial class ExchangeCrypt : Window
    {
        private double? cryptExc1 = 1;
        private double? cryptExc2 = 1;

        public ExchangeCrypt(CryptInfo crypt)
        {
            InitializeComponent();

            crypt1.Text = crypt.Name;
            cryptExc1 = (double)crypt.Price;
        }

        /// <summary>
        /// Конвертація криптовалюти
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExchange(object sender, RoutedEventArgs e)
        {
            int cnt = int.Parse(count.Text);

            if ((double)CryptViewModel.SearchCrypt(crypt2.Text).Price == null) cryptExc2 = 1;
            else cryptExc2 = (double)CryptViewModel.SearchCrypt(crypt2.Text).Price;

            double? res = (cryptExc1 * cnt) / cryptExc2;
            result.Text = res.ToString();
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