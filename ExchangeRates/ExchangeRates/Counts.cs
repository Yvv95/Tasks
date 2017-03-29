using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExchangeRates
{
    class Counts : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public double money { get; set; }
        public double ratio { get; set; }
        public double sum { get; set; }
        public Counts(double _money, double _ratio)
        {
            this.money = _money;
            this.ratio = _ratio;
            sum = money * ratio;
        }
        public double Money
        {
            get { return money; }
            set
            {
                money = value;
                Notify("Money");
            }
        }

        public double Ratio
        {
            get { return ratio; }
            set
            {
                ratio = value;
                Notify("Ratio");
            }
        }

        public double Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                Notify("Sum");
            }
        }
    }
}
