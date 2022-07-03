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

            AllCryptListView.ItemsSource = CryptViewModel.CryptInfoList?.ToList();
        }
    }
}
