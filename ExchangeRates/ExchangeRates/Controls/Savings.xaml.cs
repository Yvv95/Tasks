using System;
using System.Collections.Generic;
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

namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for Savings.xaml
    /// </summary>
    public partial class Savings : UserControl
    {
        public double money = 100;
        public double finalValuta=1;
        public int code = 0;

        public Savings()
        {
            InitializeComponent();
            comboBox.DataContext = ValuteHelper.getNames();
            //textBox.DataContext = money;
            countedSum.DataContext = code.ToString();

            //Counts convert = new Counts(0, 0);
            //countedSum.DataContext = convert;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //code = ValuteHelper.getValuteByName((sender as ComboBox).SelectedItem.ToString()).WorldName;

            money = Double.Parse(textBox.Text);
            Counts convert = new Counts(money, Double.Parse(ValuteHelper.getValuteByName(comboBox.SelectedItem.ToString()).Exchange));           
            countedSum.DataContext = convert;

        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void countedSum_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }
    }
}
