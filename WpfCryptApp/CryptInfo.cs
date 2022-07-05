using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfCryptApp
{
    public class CryptInfo : INotifyPropertyChanged
    {

        private string? market;
        private string? id;
        private string? name;
        private string? symbol;
        private decimal? price;
        private decimal? change_24h;
        private DateTime updated_datetime;

        public string? Market
        {
            get { return market; }
            set { market = value; OnPropertyChanged("Market"); }
        }

        public string? Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string? Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string? Symbol
        {
            get { return symbol; }
            set { symbol = value; OnPropertyChanged("Symbol"); }
        }        

        public decimal? Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }
        
        public decimal? Change_24h
        {
            get { return change_24h; }
            set { change_24h = value; OnPropertyChanged("Change_24h"); }
        }
        
        public DateTime Updated_DateTime
        {
            get { return updated_datetime; }
            set { updated_datetime = value; OnPropertyChanged("Updated_DateTime"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}