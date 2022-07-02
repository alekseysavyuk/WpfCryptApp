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
        private static CryptViewModel cryptViewModel = new CryptViewModel();

        public MainWindow()
        {
            /*
        <Canvas>
            <Button Content="Update" Height="40" Width="150" Canvas.Bottom="20" Canvas.Left="40" IsDefault="True" Click="Update" />
        </Canvas>
        <Canvas>
            <Button Content="Close" Height="40" Width="150" Canvas.Bottom="20" Canvas.Right="40" IsCancel="True" Click="Close" />
        </Canvas>
            */
            //crypt = new ApplicationViewModel();

            //Thread.Sleep(150);

            InitializeComponent();

            DataContext = cryptViewModel;
        }
        
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
