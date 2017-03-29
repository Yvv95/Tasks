using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExchangeRates
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _myText;

        public string MyText
        {
            get { return _myText; }
            set
            {
                _myText = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}