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
//приложение стоит при загрузке нового диапазона дат

namespace ExchangeRates
{
    public partial class MainWindow : Window
    {
        public TabChanger tabs = new TabChanger();
        private bool loading = true;
        private bool wasLoaded = false; //чтобы при первой загрузке не было пусто
        private bool codesLoaded = false;
        BackgroundWorker bw = new BackgroundWorker();//для загрузки курса в другом потоке
        private byte selectedDates = 1;//1-день, 2-неделя, 3-месяц, 4-год
        Dictionary<string, string> getWorldCode = new Dictionary<string, string>();
        private int period = 1;
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
            changingBox.Items.Add("День");
            changingBox.Items.Add("Неделя");
            changingBox.Items.Add("Месяц");
            changingBox.Items.Add("Квартал");
            changingBox.Items.Add("Год");
            bw.RunWorkerAsync();
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
            
            if (!codesLoaded)
            getWorldCode = loadCodes();
            openLoad();
        }
               
        private void openLoad()//сама загрузка с сервиса
        {
            var tmp = new CBRFServices.DailyInfoSoapClient();
            try
            {
                tmp.Open();
                // var xml = tmp.BiCurBacket(); //tables - >result view ->Rows->...           

                DateTime lastLoad = tmp.GetLatestDateTime();
                var getvalutes = tmp.GetCursOnDate(lastLoad);
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

        public double loadMiddleValue(int days, string code, string curValue)//загрузка курсов по датам(для графика)
        {
            double counts = 0;
            int nums = 0;
            var tmp = new CBRFServices.DailyInfoSoapClient();
            try
            {
                tmp.Open();
                DateTime lastLoad = tmp.GetLatestDateTime();
                DateTime prevLoad = lastLoad.AddDays(-days);
                var ofk = tmp.GetCursDynamic(prevLoad, lastLoad, getWorldCode[code]); //код евро 978
                DataTable dynamicKurse = ofk.Tables["ValuteCursDynamic"];
                //Таблица динамики: 0-время, 1-iso код, 2-какой-то номер, 3-курс            
                if (dynamicKurse.Rows.Count > 1)
                    for (int i = 0; i < (dynamicKurse.Rows.Count-1); i++)
                    {
                        counts += double.Parse(dynamicKurse.Rows[i][3].ToString());
                        nums++;
                    }
                else
                    MessageBox.Show("Не было курса в выбранный период!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка при работе с сервисом. Подробнее: \n" + e.ToString());
            }
            finally
            {
                tmp.Close();
            }
            if (nums < 1)
                nums = 1;
            return   double.Parse(curValue)- (counts / (double)nums);
        }

        private void updateValues()//загрузка на грид отмеченных валют
        {
            lastUpdate.Text = "Курс на " + ValuteHelper.whenLoaded;
            valutesPanel.Children.Clear();
            bool rises = false;
            double changed = 0;
            foreach (Valutes toShow in ValuteHelper.ValList.Values)
            {
                if (toShow.Checked)
                {
                    changed = loadMiddleValue(period, toShow.worldName.ToString(), toShow.Exchange);
                    if (changed > 0)
                    {
                        rises = true;
                        toShow.Changing = "+" + Math.Round(changed, 2).ToString();
                    }
                    else
                    {
                        rises = false;
                        toShow.Changing = Math.Round(changed, 2).ToString();
                    }
                    var newValute = new ValuteBox(toShow, rises)
                    {                                                
                        //здесь написать методы для изменения свойств
                    };
                    valutesPanel.Children.Add(newValute);
                }
            }
        }

        public Dictionary<string, string> loadCodes()//загрузка кодов
        {
            var loader = new CBRFServices.DailyInfoSoapClient();
            Dictionary<string, string> codesList = new Dictionary<string, string>();
            try
            {
                loader.Open();
                DataSet DSC = (System.Data.DataSet)loader.EnumValutes(false); //Получаем список валют
                System.Data.DataTable tbl = DSC.Tables["EnumValutes"]; //получаем саму таблицу со списком валют
                for (int i = 0; i < tbl.Rows.Count; i++)
                    if (!codesList.ContainsKey(tbl.Rows[i]["VnumCode"].ToString().Trim()))
                        codesList.Add(tbl.Rows[i]["VnumCode"].ToString().Trim(), tbl.Rows[i]["Vcode"].ToString().Trim());
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка:" + e.ToString());
            }
            finally { loader.Close(); }
            if (codesList.Count < 1)
                MessageBox.Show("Не удалось загрузить коды");
            codesLoaded = true;
            return codesList;
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

        private void changingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (changingBox.SelectedIndex)
            {
                case 0:
                    period = 1;
                    bw.RunWorkerAsync();
                    break;
                case 1:
                    period = 7;
                    bw.RunWorkerAsync();
                    break;
                case 2:
                    period = 30;
                    bw.RunWorkerAsync();
                    break;
                case 3:
                    period = 92;
                    bw.RunWorkerAsync();
                    break;
                case 4:
                    period = 365;
                    bw.RunWorkerAsync();
                    break;
            }
        }
    }
}
