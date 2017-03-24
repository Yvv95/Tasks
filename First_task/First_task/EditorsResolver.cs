using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main_Project.Controls;

namespace First_task
{
    public static class EditorsResolver
    {
        public static Dictionary<string, Type> parseEditors = new Dictionary<string, Type>();
        public static Type resolveMethod(string inType)
        {
            return parseEditors.ContainsKey(inType) ? parseEditors[inType] : typeof(StringEditingControl);
        } 
    }
}
