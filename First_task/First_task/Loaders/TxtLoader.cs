using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace First_task
{
    public class TxtLoader : ILoader
    {
        public string text="";      
        string reg = @"#(?<id>[a-zA-Z0-9]+)\((?<text>[a-zA-Zа-яА-Я0-9.]+)\)/(?<type>[a-zA-Z0-9]+)#";

        public Dictionary<string, MyDataRow> getvalues(string path)
        {
            Dictionary<string, MyDataRow> _items = null;

            using (System.IO.StreamReader sr = new
                          System.IO.StreamReader(path, System.Text.Encoding.UTF8))
            { 
            try
            {
                text = sr.ReadToEnd();
            }
            finally
            {
                sr.Close();
            }
            }
            _items = new Dictionary<string, MyDataRow>();
            List<string> _id = new List<string>();
            foreach (Match m in Regex.Matches(text, reg))
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
            string text="";
            using (System.IO.StreamReader sr = new
                      System.IO.StreamReader(templatePath, System.Text.Encoding.UTF8))
            {
                try
                {
                    text = sr.ReadToEnd();
                }
                finally
                {
                    sr.Close();
                }
            }
            
            string outFile = Regex.Replace(text, reg, m => ((changedTable[m.Groups["id"].Value].value != "")) ?
                                    changedTable[m.Groups["id"].Value].value : "значение не присвоено");
                System.IO.File.WriteAllText(path + Path.GetExtension(templatePath), outFile);        
        }
    }
}
