using Main_Project.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace First_task.Controls
{
    public class MyDataGridCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object
       initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            IMyEditControl ctl =
                DataGridView.EditingControl as IMyEditControl;
            if (ctl!=null)
            ctl.SetValue(this.Value == null ? ctl.DefaultValue : this.Value);       
        }

        public override Type EditType
        {
            get
            {
                MyDataRow tmp = this.OwningRow.DataBoundItem as MyDataRow;
                if (tmp != null)
                   return EditorsResolver.resolveMethod(tmp.type);
                else
                return typeof(StringEditingControl);

                    //switch (tmp.type)
                    //{
                    //    case "date":
                    //        return typeof(CalendarEditingControl);
                    //        case "curdate":
                    //            {
                    //                return typeof(CalendarEditingControl);
                    //            }
                    //        case "str":
                    //            {
                    //                return typeof(StringEditingControl);
                    //            }
                    //    case "int":
                    //            {
                    //                return null;//typeof(IntEditingControl);                               
                    //            }
                    //    case "check":
                    //        return typeof(YesNoEditingControl);
                    //    case "quart":
                    //            return typeof(QuartEditingControl);
                    //    default:
                    //        return typeof(StringEditingControl);
                    //}
                    //return typeof(StringEditingControl);

            }
        }

    }
}
