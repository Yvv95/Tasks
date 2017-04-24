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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chart;


namespace ExchangeRates
{
    /// <summary>
    /// Interaction logic for DynamicForm.xaml
    /// </summary>
    public partial class DynamicForm : UserControl
    {
        public DynamicForm()
        {
            InitializeComponent();

            //(X - координата, Y - координата, Подпись)
            var data = new DataPointsList<DataPoint>();
            data.Add(new DataPoint(0, 8, "0"));
            data.Add(new DataPoint(1, 4, "1"));
            data.Add(new DataPoint(2, 3, "2"));
            data.Add(new DataPoint(3, 2, "3"));
             
            this.MyData = data;
        }

        public DataPointsList<DataPoint> MyData { get; set; }

    }
}
