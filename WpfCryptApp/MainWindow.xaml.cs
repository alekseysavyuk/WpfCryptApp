﻿using System;
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
            //Thread.Sleep(100);

            InitializeComponent();

            DataContext = cryptViewModel;
        }

        private void SearchCrypt(object sender, RoutedEventArgs e)
        {
            TextBox text = (TextBox)sender;
            MessageBox.Show(text.Text);
        }
        
        private void Button_More(object sender, RoutedEventArgs e)
        {
            AllCryptWindow allCryptWindow = new AllCryptWindow();
            allCryptWindow.Show();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
