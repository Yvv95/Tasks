using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using ExchangeRates.Controls;

//ToDo
//сделать графики в dynamic
//разницу в курсе находить перебором - подгружать за один день. если одна строка, то за два дня, и т.д.


namespace ExchangeRates
{
    public partial class MainWindow : Window
    {
        public TabChanger tabs = new TabChanger();
        private bool loading = true;
        private bool wasLoaded = false; //чтобы при первой загрузке не было пусто
        BackgroundWorker bw = new BackgroundWorker();//для загрузки курса в другом потоке

        public MainWindow()
        {
            //события для воркера
            bw.DoWork +=
    new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted+= new RunWorkerCompletedEventHandler(bw_CompleteWork);
            bw.WorkerSupportsCancellation = true;
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

            bw.RunWorkerAsync();
           
            #region old
            //var tmp = new CBRFServices.DailyInfoSoapClient();
            //try
            //{
            //    tmp.Open();
            //    // var xml = tmp.BiCurBacket(); //tables - >result view ->Rows->...           
            //    DateTime lastLoad = tmp.GetLatestDateTime();
            //    var getvalutes = tmp.GetCursOnDate(lastLoad);
            //    Thread.Sleep(2000);


            //    ////////////////////////////////
            //    //для динамики
            //    double changed = 0;
            //    DataSet DSC = (System.Data.DataSet)tmp.EnumValutes(false); //Получаем список валют
            //    System.Data.DataTable tbl = DSC.Tables["EnumValutes"]; //получаем саму таблицу со списком валют
            //    Dictionary<string, string> codeGetter = new Dictionary<string, string>();
            //    //словарь - для получения кода валюты по другому её коду
            //    for (int i = 0; i < tbl.Rows.Count; i++)
            //    {
            //        if (!codeGetter.ContainsKey(tbl.Rows[i]["VnumCode"].ToString().Trim()))
            //            codeGetter.Add(tbl.Rows[i]["VnumCode"].ToString().Trim(), tbl.Rows[i]["Vcode"].ToString().Trim());
            //        //заполнили словарь
            //    }
            //    DateTime prevLoad = lastLoad.AddDays(-5); //дату на день назад. тут на 5 дней
            //    var ofk = tmp.GetCursDynamic(prevLoad, lastLoad, codeGetter["978"]); //код евро
            //    DataTable dynamicKurse = ofk.Tables["ValuteCursDynamic"];
            //    //Таблица динамики: 0-время, 1-iso код, 2-какой-то номер, 3-текущий курс            
            //    changed = double.Parse(dynamicKurse.Rows[1][3].ToString()) -
            //              double.Parse(dynamicKurse.Rows[0][3].ToString()); //изменение за день
            //    /////////////////////////////


            //    // Vname - Vcurs ;Vnom, !Vcode!, VchCode
            //    DataTable currentKurse = getvalutes.Tables["ValuteCursOnDate"];
            //    lastUpdate.Text = "Курс на " + lastLoad.ToShortDateString();

            //    for (int i = 0; i < currentKurse.Rows.Count; i++)
            //    {
            //        ValuteHelper.ValList.Add(Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()),
            //            new Valutes(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(),
            //                currentKurse.Rows[i]["Vcurs"].ToString(),
            //                Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()),
            //                currentKurse.Rows[i]["VchCode"].ToString().TrimEnd(), false));
            //        ValuteHelper.ConvList.Add(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(),
            //            new ValuteConverter(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(),
            //                Double.Parse(currentKurse.Rows[i]["Vcurs"].ToString())));
            //    }
            //    ValuteHelper.ValList[978].Checked = true; //для примера
            //    ValuteHelper.ValList[840].Checked = true;
            //    updateValues();

            //}
            //catch (Exception e)
            //{

            //    MessageBox.Show("Ошибка при работе с сервисом. Подробнее: \n" + e.ToString());
            //}
            //finally
            //{
            //    tmp.Close();
            //}
            #endregion
        }

        private void bw_CompleteWork(object sender, RunWorkerCompletedEventArgs e)//событие завершения работы
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Произошло прерывание");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Ошибка: "+e.Error.Message);
            }
            else
            {
                updateValues();
            }
            loading = false;
        }

        private void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)//сам воркер
        {
            openLoad();
        }       
        private void openLoad()//сама загрузка с сервиса
        {
           // Thread.Sleep(5000);
            var tmp = new CBRFServices.DailyInfoSoapClient();
            try
            {
                tmp.Open();
                // var xml = tmp.BiCurBacket(); //tables - >result view ->Rows->...           

                DateTime lastLoad = tmp.GetLatestDateTime();
                var getvalutes = tmp.GetCursOnDate(lastLoad);


                #region Загрузчик валют за предыдущий период 
                //////////////////////////////////
                //////для динамики
                //double changed = 0;
                //DataSet DSC = (System.Data.DataSet)tmp.EnumValutes(false); //Получаем список валют
                //System.Data.DataTable tbl = DSC.Tables["EnumValutes"]; //получаем саму таблицу со списком валют
                //Dictionary<string, string> codeGetter = new Dictionary<string, string>();
                ////словарь - для получения кода валюты по другому её коду
                //for (int i = 0; i < tbl.Rows.Count; i++)
                //{
                //    if (!codeGetter.ContainsKey(tbl.Rows[i]["VnumCode"].ToString().Trim()))
                //        codeGetter.Add(tbl.Rows[i]["VnumCode"].ToString().Trim(), tbl.Rows[i]["Vcode"].ToString().Trim());
                //    //заполнили словарь
                //}
                //DateTime prevLoad = lastLoad.AddDays(-5); //дату на день назад. тут на 5 дней
                //var ofk = tmp.GetCursDynamic(prevLoad, lastLoad, codeGetter["978"]); //код евро
                //DataTable dynamicKurse = ofk.Tables["ValuteCursDynamic"];
                ////Таблица динамики: 0-время, 1-iso код, 2-какой-то номер, 3-текущий курс            
                //changed = double.Parse(dynamicKurse.Rows[dynamicKurse.Rows.Count-1][3].ToString()) -
                //          double.Parse(dynamicKurse.Rows[dynamicKurse.Rows.Count-2][3].ToString()); //изменение за день
                /////////////////////////////////
                #endregion

                // Vname - Vcurs ;Vnom, !Vcode!, VchCode
                DataTable currentKurse = getvalutes.Tables["ValuteCursOnDate"];

                ValuteHelper.whenLoaded = lastLoad.ToShortDateString();

                if (!wasLoaded)
                {
                    for (int i = 0; i < currentKurse.Rows.Count; i++)
                    {
                        if (!ValuteHelper.ValList.ContainsKey(Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString())))
                        {
                            ValuteHelper.ValList.Add(Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()),
                                new Valutes(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(),
                                    currentKurse.Rows[i]["Vcurs"].ToString(),
                                    Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString()),
                                    currentKurse.Rows[i]["VchCode"].ToString().TrimEnd(), false));
                            ValuteHelper.ConvList.Add(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(),
                                new ValuteConverter(currentKurse.Rows[i]["Vname"].ToString().TrimEnd(),
                                    Double.Parse(currentKurse.Rows[i]["Vcurs"].ToString())));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < currentKurse.Rows.Count; i++)
                    {
                        if (!ValuteHelper.ValList.ContainsKey(Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString())))
                        {
                            ValuteHelper.ValList[Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString())]
                                .updateExchange(currentKurse.Rows[i]["Vcurs"].ToString());

                            //ValuteHelper.ValList[Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString())] =
                            //    ValuteHelper.ValList[Int32.Parse(currentKurse.Rows[i]["Vcode"].ToString())];

                            ValuteHelper.ConvList[currentKurse.Rows[i]["Vname"].ToString().TrimEnd()].updateExchange(Double.Parse(currentKurse.Rows[i]["Vcurs"].ToString()));
                                
                        }
                    }
                }
                if (!wasLoaded)
                {
                    ValuteHelper.ValList[978].Checked = true; //для примера
                    ValuteHelper.ValList[840].Checked = true;
                    wasLoaded = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе с сервисом. Подробнее: \n" + e.ToString());
            }
            finally
            {
                tmp.Close();
            }
        }


        

        private void updateValues()//загрузка на грид отмеченных валют
        {
            lastUpdate.Text = "Курс на " + ValuteHelper.whenLoaded;
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

        private void reloadButton_Click(object sender, RoutedEventArgs e)//обновить курс
        {
            valutesPanel.Children.Clear();
            lastUpdate.Text = "Обновление...";
            bw.DoWork +=
   new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_CompleteWork);
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerAsync();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!loading)
            {
                tabs.TabSwitch(Tabs.SelectedIndex + 1);

                switch (Tabs.SelectedIndex)
                {
                    case 0:
                        valutesGrid.Children.Clear();
                        updateValues();
                        break;
                    case 1:
                        DynamicForm changingForm = new DynamicForm();
                        valutesGrid.Children.Add(changingForm);
                        //здесь будет переключение на вкладку с динамикой
                        break;
                    case 2:
                        SavingForm savingWindows = new SavingForm();
                        valutesGrid.Children.Add(savingWindows);
                        break;
                    case 3:
                        Settings settingsWindows = new Settings();
                        valutesGrid.Children.Add(settingsWindows);
                        break;
                }
            }
        }

        private void inputGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void inputGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        

        
    }
}
