using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using ExchangeRates.Controls;

//во вкладке с выбором сделать TemplatePanel

//сделать относительный путь в Image
//доделать загрузку формы с конвертором

namespace ExchangeRates
{
    public partial class MainWindow : Window
    {
        public TabChanger tabs = new TabChanger();

        public MainWindow()
        {
            InitializeComponent();
            var tmp = new CBRFServices.DailyInfoSoapClient();

            tabs.TabSwitch(1);
            mainButton.DataContext = tabs;
            diagramButton.DataContext = tabs;
            savingsButton.DataContext = tabs;
            settingButton.DataContext = tabs;
            

            List<string> columnsList = new List<string>();
            try
            {
               tmp.Open();
               // var xml = tmp.BiCurBacket(); //tables - >result view ->Rows->...           
                DateTime lastLoad = tmp.GetLatestDateTime();
                var getvalutes = tmp.GetCursOnDate(lastLoad);                

                // Vname - Vcurs ;Vnom, !Vcode!, VchCode
                DataTable currentKurse = getvalutes.Tables["ValuteCursOnDate"];
                lastUpdate.Text = "Курс на " + lastLoad;

                //<=  ?  
                for (int i = 0; i < currentKurse.Rows.Count; i++)
                {
                    ValuteHelper.ValList.Add(Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()), new Valutes(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(), currentKurse.Rows[i]["Vcurs"].ToString(), Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()), currentKurse.Rows[i]["VchCode"].ToString().TrimEnd(), false));
                    ValuteHelper.ConvList.Add(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(), new ValuteConverter(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(), Double.Parse(currentKurse.Rows[i]["Vcurs"].ToString())));
                }
                ValuteHelper.ValList[978].Checked = true;
                ValuteHelper.ValList[840].Checked = true;           
                //comboBox.DataContext = ValuteHelper.getNames();
                updateValues();   
                //var listValutes = tmp.EnumValutes(true);              
            }
            catch
            {
                tmp.Close(); //делать с try catch
            }
        }

        private void updateValues()
        {
            ///
            valutesPanel.Children.Clear();
            /// 
            foreach (Valutes toShow in ValuteHelper.ValList.Values)
            {
                if (toShow.Checked)
                {
                    var newValute = new ValuteBox(toShow)
                    {
                        //здесь написать методы для изменения свойств
                    };
                    valutesPanel.Children.Add(newValute);
                }
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)//доход
        {
            tabs.TabSwitch(3);

            //valutesGrid.Children.Clear();
            SavingForm savingWindows = new SavingForm();
            valutesGrid.Children.Add(savingWindows);
        }

        private void mainButton_Click(object sender, RoutedEventArgs e)
        {
            tabs.TabSwitch(1);
            valutesGrid.Children.Clear();
            updateValues();
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            tabs.TabSwitch(4);


            //stackPanel.Children.Clear();
            //valutesGrid.Children.Clear();
            //valGrid.Children.Clear();
            Settings settingsWindows = new Settings();
            valutesGrid.Children.Add(settingsWindows);
            //valutesGrid.Children.Add(settingsWindows);
            //valutesGrid.Height = settingsWindows.Height;
        }

        private void diagramButton_Click(object sender, RoutedEventArgs e)
        {
            tabs.TabSwitch(2);
            //valutesGrid
            //valutesGrid.Children.Clear();
        }

       
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //stackPanel.Width = e.NewSize.Width;

            //stackPanel.Height = e.NewSize.Height;
        }
        
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
