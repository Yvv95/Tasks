using System;
using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace First_task
{
    public class ExcelLoader : ILoader
    {
        public string text = "";
        string reg = @"#(?<id>[a-zA-Z0-9]+)\((?<text>[a-zA-Zа-яА-Я0-9.]+)\)/(?<type>[a-zA-Z0-9]+)#";

        public Dictionary<string, MyDataRow> getvalues(string path)
        {
            //try finally, чтобы корректно завершалось
            string[,] list = new string[1, 1];
            int lastColumn = 1;
            int lastRow = 1;      
            Dictionary<string, MyDataRow> _items = new Dictionary<string, MyDataRow>();           
            Microsoft.Office.Interop.Excel.Application ObjWorkExcel = new Microsoft.Office.Interop.Excel.Application(); //открыть эксель
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //открыть файл
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1 лист
            var lastCell = ObjWorkSheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);//1 ячейку
            //именованные области
            for (int i = 1; i <= ObjWorkSheet.Names.Count; i++)
            {
                var smth = ObjWorkBook.Names.Item(i, Type.Missing, Type.Missing).RefersToRange.Value2;
                MessageBox.Show(smth.GetLength(0).ToString() + " " + smth.GetLength(1).ToString());
                lastRow = smth.GetLength(0);
                lastColumn = smth.GetLength(1);
                list = new string[lastRow+1, lastColumn+1];
                for (int ii = 0; ii < lastRow; ii++)
                    for (int j = 0; j < lastColumn; j++)
                    {
                        if (smth[ii + 1, j + 1] is string)
                            list[ii, j] = smth[ii + 1, j + 1].ToString();
                        else
                        list[ii, j] = " ";                  
                    }
            }             
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
            ObjWorkExcel.Quit();
            ObjWorkSheet = null;
            GC.Collect();
            List<string> _id = new List<string>();

            for (int i = 0; i < lastRow; i++)
                for (int j = 0; j < lastColumn; j++)
                    foreach (Match m in Regex.Matches(list[i, j], reg))
                    {
                        if (!_id.Contains(m.Groups["id"].Value))
                        {
                            _id.Add(m.Groups["id"].Value);
                            _items.Add(m.Groups["id"].Value, new MyDataRow(m.Groups["id"].Value, "", m.Groups["text"].Value, m.Groups["type"].Value));
                        }
                    }
            return _items;
        }


        public void setvalues(string path, string templatePath, Dictionary<string, MyDataRow> changedTable)
        {
            string[,] list = new string[1, 1];
            int lastColumn = 1;
            int lastRow = 1;
            object misValue = System.Reflection.Missing.Value;
            //чтение файла-шаблона
            Microsoft.Office.Interop.Excel.Application ObjWorkExcel = new Microsoft.Office.Interop.Excel.Application(); //открыть эксель
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjWorkExcel.Workbooks.Open(templatePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //открыть файл
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1 лист
            var lastCell = ObjWorkSheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell);//1 ячейку
            for (int i = 1; i <= ObjWorkSheet.Names.Count; i++)
            {
                var smth = ObjWorkBook.Names.Item(i, Type.Missing, Type.Missing).RefersToRange.Value2;
                MessageBox.Show(smth.GetLength(0).ToString() + " " + smth.GetLength(1).ToString());
                lastRow = smth.GetLength(0);
                lastColumn = smth.GetLength(1);
                list = new string[lastRow + 1, lastColumn + 1];
                for (int ii = 0; ii < lastRow; ii++)
                    for (int j = 0; j < lastColumn; j++)
                    {
                        if (smth[ii + 1, j + 1] is string)
                            list[ii, j] = smth[ii + 1, j + 1].ToString();
                        else
                            list[ii, j] = " ";
                    }
            }
           
            ObjWorkBook.Close(false, Type.Missing, Type.Missing);
            ObjWorkExcel.Quit();
            ObjWorkSheet = null;
            GC.Collect();

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            for (int i = 0; i < lastRow; i++)
            {
                for (int j = 0; j < lastColumn; j++)
                {
                    if (list[i, j] != null)
                        list[i, j] = Regex.Replace(list[i, j], reg, m => ((changedTable[m.Groups["id"].Value].value != "")) ?
                        changedTable[m.Groups["id"].Value].value : list[i, j]);
                    ExcelApp.Cells[i + 1, j + 1] = list[i, j];
                }
            }
            ExcelApp.Visible = false;
            ExcelApp.UserControl = false;
            ExcelWorkBook.SaveAs(path + Path.GetExtension(templatePath), Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
        false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ExcelWorkBook.Close(true, misValue, misValue);
            ExcelWorkSheet = null;
            ExcelApp.Quit();
            ExcelApp = null;
        }
    }
}
