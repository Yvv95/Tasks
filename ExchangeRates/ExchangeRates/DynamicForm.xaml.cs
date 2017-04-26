using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chart;
using MessageBox = System.Windows.MessageBox;


namespace ExchangeRates
{
    //TODO
    //вынести загрузку данных в отдельный метод(с параметрами)
    //загрузку с помощью backGroundWorker - исправить, чтобы не стопорилось и блочить другие вкладки во время загрузки
    //график привести в нормальный вид

    public partial class DynamicForm : UserControl
    {
        BackgroundWorker bw = new BackgroundWorker();
        private int period=1;
        Dictionary<string, string> codeGetter = new Dictionary<string, string>();//для перевода номера валюты в другой формат, чтобы загрузить курс за прошлые даты
        private bool codesLoaded = false;
        Dictionary<string, double> loadedPoints = new Dictionary<string, double>();
        string curGraphic = "978";//Евро
        public DynamicForm()
        {
            InitializeComponent();
            bw.DoWork +=
    new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_CompleteWork);
            bw.WorkerSupportsCancellation = true;
            periodBox.Items.Add("Неделя");
            periodBox.Items.Add("Месяц");
            periodBox.Items.Add("Квартал");
            periodBox.Items.Add("Год");
            periodBox.SelectedIndex = 0;

            

            ValuteListConverter listVal = new ValuteListConverter();
            foreach (ValuteConverter val in ValuteHelper.ConvList.Values)
            {
                listVal.AddValues(val.Names, val.exchange);
            }
            selectBox.DataContext = listVal;
           
        }

        private void bw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            
            loadedPoints = loadPoints(period);
        }

        private void bw_CompleteWork(object sender, RunWorkerCompletedEventArgs e)//событие завершения работы
        {
            if (e.Cancelled == true)
            {
                MessageBox.Show("Произошло прерывание");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Ошибка: " + e.Error.Message);
            }
            else
            {
                int counter = 0;
                foreach (var _point in loadedPoints)              
                    graphicChart.Areas[0].Series[0].DataPoints.Add(new DataPoint(++counter, _point.Value, DateTime.Parse(_point.Key).ToShortDateString()));                               
            }
        }

        public Dictionary<string, double> loadPoints(int days)
        {
            double changed = 0;
            Dictionary<string, double> chng = new Dictionary<string, double>();
            var tmp = new CBRFServices.DailyInfoSoapClient();
            try
            {
                tmp.Open();
                // var xml = tmp.BiCurBacket(); //tables - >result view ->Rows->...           
                DateTime lastLoad = tmp.GetLatestDateTime();
               
                if (!codesLoaded)
                {
                    DataSet DSC = (System.Data.DataSet)tmp.EnumValutes(false); //Получаем список валют
                    System.Data.DataTable tbl = DSC.Tables["EnumValutes"]; //получаем саму таблицу со списком валют
                    for (int i = 0; i < tbl.Rows.Count; i++)
                        if (!codeGetter.ContainsKey(tbl.Rows[i]["VnumCode"].ToString().Trim()))
                            codeGetter.Add(tbl.Rows[i]["VnumCode"].ToString().Trim(), tbl.Rows[i]["Vcode"].ToString().Trim());
                    codesLoaded = true;
                }
                DateTime prevLoad = lastLoad.AddDays(-days); 
                var ofk = tmp.GetCursDynamic(prevLoad, lastLoad, codeGetter["978"]); //код евро
                DataTable dynamicKurse = ofk.Tables["ValuteCursDynamic"];
                //Таблица динамики: 0-время, 1-iso код, 2-какой-то номер, 3-курс            

                if (dynamicKurse.Rows.Count > 0)
                for (int i = 0; i < dynamicKurse.Rows.Count; i++)                
                    chng.Add(dynamicKurse.Rows[i][0].ToString(), double.Parse(dynamicKurse.Rows[i][3].ToString()));         
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
            return chng;
        }



        private void selectBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curGraphic = ValuteHelper.getValuteByName((string) selectBox.SelectedItem).WorldName.ToString();
         //   MessageBox.Show((string)selectBox.SelectedItem);
            loadedPoints = new Dictionary<string, double>();
            graphicChart.Areas[0].Series[0].DataPoints.Clear();
            bw.RunWorkerAsync();
        }

        private void periodBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (periodBox.SelectedIndex)
            {
                case 0:
                    period = 7;
                    break;
                case 1:
                    period = 30;
                    break;
                case 2:
                    period = 90;
                    break;
                case 3:
                    period = 365;
                    break;
            }
            loadedPoints = new Dictionary<string, double>();
            graphicChart.Areas[0].Series[0].DataPoints.Clear();
            bw.RunWorkerAsync();
        }
    }
}
