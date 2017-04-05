using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExchangeRates.Controls;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace ExchangeRates
{
    public partial class Savings : UserControl
    {
        public double money = 100;
        public double finalValuta = 1;
        public int code = 0;

        public Savings(Valutes val)
        {
            InitializeComponent();
            comboBox.DataContext = ValuteHelper.getNames();
      
              
            }

        //метод, чтобы вводились только цифры
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0) || (e.Text == ","))
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           //сделать отдельное поле в классе Valutes для отображения и прибиндиться на него?

        }
    }
}
