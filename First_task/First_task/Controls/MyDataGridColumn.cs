using First_task.Controls;
using System;
using System.Windows.Forms;
public class MyDataGridColumn : DataGridViewColumn
{
    public MyDataGridColumn() : base(new MyDataGridCell())
    {
    }

    public override DataGridViewCell CellTemplate
    {
        get
        {
            return base.CellTemplate;
        }
        set
        {
            if (value != null &&
                !value.GetType().IsAssignableFrom(typeof(MyDataGridCell)))
            {
                throw new InvalidCastException("Не MyDataGridCell");
            }
            base.CellTemplate = value;
        }
    }
}