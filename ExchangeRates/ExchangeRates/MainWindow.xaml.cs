using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class MainWindow : Window
    {
        //public static Dictionary<int, bool> selectedValute = new Dictionary<int, bool>();//для настроек - какие валюты отображать

        //public static Dictionary<int, Valutes> ValList = new Dictionary<int, Valutes>();//ключ - название валюты
        public MainWindow()
        {
            InitializeComponent();
            var tmp = new CBRFServices.DailyInfoSoapClient();
           
            List<string> columnsList = new List<string>();
            try
            {
                tmp.Open();
               // var xml = tmp.BiCurBacket(); //tables - >result view ->Rows->...           
                DateTime lastLoad = tmp.GetLatestDateTime();
                var getvalutes = tmp.GetCursOnDate(lastLoad);                

                //получаем названия колонок
                for (int i=0; i<getvalutes.Tables["ValuteCursOnDate"].Columns.Count; i++)
                    columnsList.Add(getvalutes.Tables["ValuteCursOnDate"].Columns[i].ColumnName); // Vname - Vcurs ;Vnom, !Vcode!, VchCode
                //доллар - 8, евро - 9
                DataTable currentKurse = getvalutes.Tables["ValuteCursOnDate"];
                lastUpdate.Text = "Актуально на " + lastLoad;

                //33 строки?
                for (int i = 0; i < currentKurse.Rows.Count; i++)
                {
                    ValuteHelper.selectedValute.Add(Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()), false);
                    ValuteHelper.ValList.Add(Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()), new Valutes(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(), currentKurse.Rows[i]["Vcurs"].ToString(), Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString())));                   
                }

                Valutes viewModel = ValuteHelper.ValList[840];//доллар сша - 840
                exchTextBox.DataContext = viewModel;
                dollarLabel.DataContext = viewModel;

                Valutes euroView = ValuteHelper.ValList[978];//евро 978
                exchTextBox2.DataContext = euroView;
                euroLabel.DataContext = euroView;
           
                comboBox.DataContext = ValuteHelper.getNames();
                

                #region что-то непонятное

                /*
                                /////
                                Valutes tmpValute = ValList[8];
                
                                Binding myBinding = new Binding();
                                //myBinding.Source = curKurse["Американский доллар"];
                                myBinding.Source = tmpValute;
                                myBinding.Path = new PropertyPath("name");
                
                                //textBlock.DataContext = ValList[8];
                                textBlock.SetBinding(TextBlock.TextProperty, myBinding);
                                ////
                                */
                //MyViewModel dollarClass = new MyViewModel();
                //dollarClass.MyText = "sdfsdfsdf";
                //changed = "fds5y";


                //MyData myDataObject = new MyData(DateTime.Now);
                //Binding myBinding1 = new Binding("MyDataProperty");
                //myBinding1.Source = myDataObject;
                //textBlock.SetBinding(TextBlock.TextProperty, myBinding1);

                #endregion

                //var listValutes = tmp.EnumValutes(true);              
            }
            catch
            {
                tmp.Close(); //делать с try catch
            }
        }


        #region ещё какой-то бред
        static void BindText(TextBox textBox, string property)
        {
            DependencyProperty textProp = TextBox.TextProperty;
            if (!BindingOperations.IsDataBound(textBox, textProp))
            {
                Binding b = new Binding(property);
                BindingOperations.SetBinding(textBox, textProp, b);
            }
        }

        #endregion

        private void button_Click(object sender, RoutedEventArgs e)//доход
        {
            stackPanel.Children.Clear();
            Savings savingWindows = new Savings();
            stackPanel.Children.Add(savingWindows);
        }

        private void mainButton_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
            Settings settingsWindows = new Settings();
            stackPanel.Children.Add(settingsWindows);
        }

        private void diagramButton_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.Children.Clear();
        }
    }
}
