using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExchangeRates
{
    public class Valutes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string name { get; set; }
        public string exchange { get; set; }
        public int worldName { get; set; }

        public Valutes(string _name, string _exchange, int _worldName)
        {
            this.name = _name;
            this.exchange = _exchange;
            this.worldName = _worldName;
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify("Name");
            }
        }

        public string Exchange
        {
            get { return exchange; }
            set
            {
                exchange = value;
                Notify("Exchange");
            }
        }

        public int WorldName
        {
            get { return worldName; }
            set
            {
                worldName = value;
                Notify("WorldName");
            }
        }

       
    }
}
