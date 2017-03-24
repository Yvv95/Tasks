using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.ComponentModel;
using Microsoft.Office.Interop.Word;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.IO;
using MEFcommon;
using First_task;

//todo
//Loader to MEF

//done


namespace First_task
{
    public partial class MainForm : Form
    {
        private Dictionary<string, MyDataRow> _items = null;
        private bool redCells = true;
        private string filePath = "";
        private const string shortPath = "Файл: ", columnName = "newValue";
        #region MEF Controls
        private const string editorsPath = @"editors";
        private CompositionContainer _container;
        [ImportMany]
        IEnumerable<Lazy<IUserEditor, ILoaderMetadata>> editors;
        private void fillData()
        { 
            //An aggregate catalog that combines multiple catalogs 
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class 
            if (!Directory.Exists(editorsPath))
                Directory.CreateDirectory(editorsPath);
            catalog.Catalogs.Add(new DirectoryCatalog(editorsPath));
            //Create the CompositionContainer with the parts in the catalog 
            _container = new CompositionContainer(catalog);
            //Fill the imports of this object 
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                MessageBox.Show(compositionException.ToString());
            }

            foreach (var editor in editors)
                EditorsResolver.parseEditors.Add(editor.Metadata.forType, editor.Value.GetType());
        }
        #endregion

        #region MEF Loaders
        private const string loadersPath = @"editors";
        private CompositionContainer _loadContainer;
        [ImportMany]
        IEnumerable<Lazy<ILoader, ILoaderfileMetadata>> loadEditors;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            fillData();
            Load += new EventHandler(Form1_Load);
            SizeChanged += new EventHandler(Form1_SizeChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void dataGrid_Leave(object sender, System.EventArgs e)
        {

        }

        //действия при загрузке формы
        private void Form1_Load(object sender, EventArgs e)
        {
            pathLabel.Text = shortPath;
            StripSaveButton.Visible = false;
            StripCloseButton.Visible = false;
            dataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//чтобы ячейка с вводимыми значениями подстраивалась автоматом          
            //dataGrid.Rows.RemoveAt(1);

        }
        private void Form1_SizeChanged(object sender, System.EventArgs e)
        {
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        //запись
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                for (int i=0; i<_items.Count; i++)
                {
                    _items.ElementAt(i).Value.value = _items.ElementAt(i).Value.getValue();
                }
                MainLoader.setvalues(saveFileDialog1.FileName, filePath, _items);            
                StripCloseButton.Visible = true;
            }
        }
        
        //чтение 
        private void toolStripButton1_Click(object sender, EventArgs e)
        {      
            dataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            pathLabel.Text = shortPath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pathLabel.Text += openFileDialog1.FileName;
                filePath = openFileDialog1.FileName;
                dataGrid.AutoGenerateColumns = false;
                _items = MainLoader.getvalues(openFileDialog1.FileName);
                dataGrid.DataSource = _items.Values.Where(elem => elem.isDisplaable()).ToArray();
                StripSaveButton.Visible = false;
                StripCloseButton.Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        //кнопка закрыть
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            _items = null;
            dataGrid.DataSource = null;
            pathLabel.Text = shortPath;
            StripCloseButton.Visible = false;
            StripSaveButton.Visible = false;
        }

        //действия по окончанию редактирования ячейки
        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            redCells = false;
            //в 3м столбце находится новое значение
            MyDataRow tmp = (dataGrid.Rows[e.RowIndex].DataBoundItem as MyDataRow);


            dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = tmp.IsValid() ? System.Drawing.Color.White : System.Drawing.Color.Red;
            for (int i = 0; i < dataGrid.RowCount; i++)
            {
                if ((dataGrid.Rows[i].Cells[3].Value != null)&&(dataGrid.Rows[i].Cells[3].Value.ToString().Length > 0))
                { }
                else
                {
                    redCells=true;
                    dataGrid.Rows[i].Cells[3].Style.BackColor = System.Drawing.Color.Red;
                }
            }
            if (redCells)
                StripSaveButton.Visible = false;
            else
                StripSaveButton.Visible = true;
        }


        private void outSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        //каждую новую строку красим
        private void dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGrid.Rows[e.RowCount-1].Cells[3].Style.BackColor = System.Drawing.Color.Red;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {           
        }
    }
}
