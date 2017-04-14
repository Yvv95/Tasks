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

//ToDo
//обновлять combobox на вкладке с конвертером, чтобы выбранных не было в списке
//панель с вкладками подгружать в itemSource
//убрать верхнюю панель с названием
//подогнать по размерам под шаблон
//полупрозрачный separator и combobox
//кастомный title bar

namespace ExchangeRates
{
    public partial class MainWindow : Window
    {
        public TabChanger tabs = new TabChanger();
        public MainWindow()
        {
            InitializeComponent();
            tabs.TabSwitch(1);
            mainButton.DataContext = tabs;
            diagramButton.DataContext = tabs;
            savingsButton.DataContext = tabs;
            settingButton.DataContext = tabs;
            firstL.DataContext = tabs;
            secondL.DataContext = tabs;
            thirdL.DataContext = tabs;
            fourthL.DataContext = tabs;

            var tmp = new CBRFServices.DailyInfoSoapClient();
            try
            {
               tmp.Open();
               // var xml = tmp.BiCurBacket(); //tables - >result view ->Rows->...           
                DateTime lastLoad = tmp.GetLatestDateTime();
                var getvalutes = tmp.GetCursOnDate(lastLoad);


                ////////////////////////////////
                //для динамики
                double changed = 0;
                DataSet DSC = (System.Data.DataSet)tmp.EnumValutes(false); //Получаем список валют
                System.Data.DataTable tbl = DSC.Tables["EnumValutes"];//получаем саму таблицу со списком валют
                Dictionary <string, string> codeGetter = new Dictionary<string,string>();//словарь - для получения кода валюты по другому её коду
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    if (!codeGetter.ContainsKey(tbl.Rows[i]["VnumCode"].ToString().Trim()))
                    codeGetter.Add(tbl.Rows[i]["VnumCode"].ToString().Trim(), tbl.Rows[i]["Vcode"].ToString().Trim());//заполнили словарь
                }
                DateTime prevLoad = lastLoad.AddDays(-1);//дату на день назад
                var ofk = tmp.GetCursDynamic(prevLoad, lastLoad, codeGetter["978"]);//код евро
                DataTable dynamicKurse = ofk.Tables["ValuteCursDynamic"]; //Таблица динамики: 0-время, 1-iso код, 2-какой-то номер, 3-текущий курс            
                changed = double.Parse(dynamicKurse.Rows[1][3].ToString()) - double.Parse(dynamicKurse.Rows[0][3].ToString());//изменение за день
                /////////////////////////////


                // Vname - Vcurs ;Vnom, !Vcode!, VchCode
                DataTable currentKurse = getvalutes.Tables["ValuteCursOnDate"];
                lastUpdate.Text = "Курс на " + lastLoad.ToShortDateString();
         
                for (int i = 0; i < currentKurse.Rows.Count; i++)
                {
                    ValuteHelper.ValList.Add(Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()), new Valutes(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(), currentKurse.Rows[i]["Vcurs"].ToString(), Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()), currentKurse.Rows[i]["VchCode"].ToString().TrimEnd(), false));
                    ValuteHelper.ConvList.Add(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(), new ValuteConverter(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(), Double.Parse(currentKurse.Rows[i]["Vcurs"].ToString())));
                }
                ValuteHelper.ValList[978].Checked = true;//для примера
                ValuteHelper.ValList[840].Checked = true;           
                updateValues();             
            }
            catch (Exception e)
            {
                tmp.Close();
                MessageBox.Show("Ошибка при работе с сервисом. Подробнее: \n"+e.ToString());
            }
        }

        private void updateValues()
        {
            valutesPanel.Children.Clear();
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
            Settings settingsWindows = new Settings();
            valutesGrid.Children.Add(settingsWindows);

        }

        private void diagramButton_Click(object sender, RoutedEventArgs e)
        {
            tabs.TabSwitch(2);
            //тут будет вкладка с динамикой
        }

       
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
        
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
