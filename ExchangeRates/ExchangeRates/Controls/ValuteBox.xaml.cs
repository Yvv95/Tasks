using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
using Brushes = System.Windows.Media.Brushes;

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for ValuteBox.xaml
    /// </summary>
    public partial class ValuteBox : UserControl
    {
        public ValuteBox(Valutes toShow, bool rose)
        {
            InitializeComponent();          
            nameBox.DataContext = toShow;
            image.DataContext = toShow;
            changingLabel.DataContext = toShow;
            kurseBox.DataContext = toShow;

            if (rose)
                changingLabel.Foreground = Brushes.Green;
            else
            changingLabel.Foreground = Brushes.Red;


            nameBox.MaxWidth = 100;
            nameBox.MaxHeight = 50;         
            image.Stretch = Stretch.Fill;  
        }        
    }
}
