using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_task
{
    public interface ILoader
    {
        Dictionary<string, MyDataRow> getvalues(string path);
        void setvalues(string path,  string templatePath, Dictionary<string, MyDataRow> changedTable);  
    }
}
