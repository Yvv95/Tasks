using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ExchangeRates.Controls
{
    /// <summary>
    /// Interaction logic for SavingForm.xaml
    /// </summary>
    public partial class SavingForm : UserControl
    {
        ObservableCollection<ValuteConverter> listConvert = new ObservableCollection<ValuteConverter>();

        public SavingForm()
        {
            InitializeComponent();
            int count = 1;


            //for (int i=0; i<5; i++)
            //    listConvert.Add(ValuteHelper.ConvList.ElementAt(i).Value);
            lineControl.DataContext = listConvert;


            newValuteBox.DataContext = ValuteHelper.getNames();

            //foreach (Valutes checking in ValuteHelper.ValList.Values)
            //{
            //    count++;
            //    Savings a = new Savings(checking);
            //    savingPanel.Children.Add(a);
            //    if (count == 5)
            //        break;
            //}
        }

        private void newValuteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         listConvert.Add(ValuteHelper.ConvList[(string)newValuteBox.SelectedItem.ToString().Trim()]);              
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
