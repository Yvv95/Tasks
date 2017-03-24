using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace First_task
{
    public static class MainLoader
    {

        public static EnumLoaderTypes whatType(string path)
        {
            string type = Path.GetExtension(path);
           // MessageBox.Show(type);
            type = type.ToLower();//lowercase        
            switch (type)
            {
                case ".txt":        
                    return EnumLoaderTypes.txt;
                case ".doc":              
                case ".docx":
                    return EnumLoaderTypes.word;
                case ".xls":                  
                case ".xlsx":
                    return EnumLoaderTypes.excel;
                default:
                    return EnumLoaderTypes.unknown;
            }
        }

        public static Dictionary<string, MyDataRow> getvalues(string path)
        {
            switch (whatType(path))
            {
                case EnumLoaderTypes.txt:
                    {
                        TxtLoader load = new TxtLoader();
                        return load.getvalues(path);
                    }

                case EnumLoaderTypes.word:
                    {
                        WordLoader load = new WordLoader();
                        return load.getvalues(path);
                    }

                case EnumLoaderTypes.excel:
                    {
                        ExcelLoader load = new ExcelLoader();
                        return load.getvalues(path);
                    }

                default:
                    return new Dictionary<string, MyDataRow>();
            }
        }

        public static void setvalues(string path, string templatePath, Dictionary<string, MyDataRow> changedTable)
        {
            switch (whatType(templatePath))
            {
                case EnumLoaderTypes.txt:
                    {
                        TxtLoader load = new TxtLoader();
                        load.setvalues(path, templatePath, changedTable);
                        break;
                    }
                case EnumLoaderTypes.word:
                    {
                        WordLoader load = new WordLoader();
                        load.setvalues(path, templatePath, changedTable);
                        break;
                    }
                case EnumLoaderTypes.excel:
                    {
                        ExcelLoader load = new ExcelLoader();
                        load.setvalues(path, templatePath, changedTable);
                        break;
                    }
                default: break;
            }
        }
    }
}
