using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExchangeRates
{

    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            checkPanel.Children.Clear();
            
            #region delete
            //setGrid.ColumnDefinitions.Clear();
            //setGrid.RowDefinitions.Clear();

            // ColumnDefinition tmpCol = new ColumnDefinition();
            // tmpCol.Width = new GridLength(200, GridUnitType.Star);
            //setGrid.ColumnDefinitions.Add(tmpCol);


            //int rows = 1;
            //  int columns = 1;
            // int splits = (ValuteHelper.ValList.Values.Count / 2);
            #endregion
            foreach (Valutes checking in ValuteHelper.ValList.Values)
            {
                #region delete
                //if ((rows > splits)&&(columns==1))
                //{
                //    ColumnDefinition tmpCols = new ColumnDefinition();
                //    tmpCols.Width = new GridLength(200, GridUnitType.Star);
                //    setGrid.ColumnDefinitions.Add(tmpCols);
                //    rows = 1;
                //    columns++;
                //}

                //setGrid.RowDefinitions.Add(new RowDefinition());
                #endregion
                CheckBox choose = new CheckBox();      
                choose.HorizontalAlignment = HorizontalAlignment.Left;

                choose.Content = checking.Name;
                Binding bind = new Binding();
                bind.Source = checking;
                bind.Path = new PropertyPath("Checked");
                bind.Mode = BindingMode.TwoWay;
                choose.SetBinding(CheckBox.IsCheckedProperty, bind);               
                choose.FontSize = 16;
                
                checkPanel.Children.Add(choose);   

                #region delete
                //Grid.SetColumn(choose, columns-1);
                //Grid.SetRow(choose, rows-1);
                //rows++;

                //ValuteCheckedPanel.Children.Add(choose);
                #endregion
            }          
        }
    }
}
