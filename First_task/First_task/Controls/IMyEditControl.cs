using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_task.Controls
{
    interface IMyEditControl
    {
        void SetValue(object value);
        object DefaultValue{ get; }
    }
}
