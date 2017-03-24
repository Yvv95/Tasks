using System;
using System.Collections.Generic;
using System.Linq;

using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace First_task
{
    public class WordLoader : ILoader
    {
        public string text = "";
        string reg = @"#(?<id>[a-zA-Z0-9]+)\((?<text>[a-zA-Zа-яА-Я0-9.]+)\)/(?<type>[a-zA-Z0-9]+)#";
        public Dictionary<string, MyDataRow> getvalues(string path)
        {       
            Dictionary<string, MyDataRow> _items = null;       
            Microsoft.Office.Interop.Word.Application wordapp = new Microsoft.Office.Interop.Word.Application();
            wordapp.Visible = false;
            Object filename = path;
            Object confirmConversions = true;
            Object readOnly = false;
            Object addToRecentFiles = true;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = false;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object oVisible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = false;
            Object xmlTransform = Type.Missing;
            var worddocument = wordapp.Documents.Open(ref filename,
            ref confirmConversions, ref readOnly, ref addToRecentFiles,
            ref passwordDocument, ref passwordTemplate, ref revert,
            ref writePasswordDocument, ref writePasswordTemplate,
            ref format, ref encoding, ref oVisible,
            ref openAndRepair, ref documentDirection, ref noEncodingDialog, ref xmlTransform);

            Object start = Type.Missing;
            Object end = Type.Missing;
            Microsoft.Office.Interop.Word.Range wordrange = worddocument.Range(ref start, ref end);
            wordrange.Select();
            wordrange.Copy();
            object unit;
            object extend;
            unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            extend = Microsoft.Office.Interop.Word.WdMovementType.wdMove;
            wordapp.Selection.EndKey(ref unit, ref extend);
            text = wordrange.Text.ToString();
            object missing = System.Reflection.Missing.Value;
            wordapp.Quit(ref missing, ref missing, ref missing);
            wordapp = null;

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
            var app = new Microsoft.Office.Interop.Word.Application();
            string temp = templatePath;
            Object fileName = temp;
            app.Documents.Open(ref fileName);
            Object missing = Type.Missing;
            Find find = app.Selection.Find;
            
            for (int j = 0; j < changedTable.Count; j++)
            {
                var a = changedTable.ElementAt(j).Value.id;
                app.Selection.Find.ClearFormatting();
                find.Text = "#" + changedTable.ElementAt(j).Value.id.ToString() + "(" + changedTable.ElementAt(j).Value.text.ToString() + ")" + "/" + changedTable.ElementAt(j).Value.type.ToString() + "#";//"Текст поиска"
                app.Selection.Find.Replacement.ClearFormatting();
                find.Replacement.Text = changedTable.ElementAt(j).Value.value.ToString();
                find.Execute(FindText: Type.Missing, MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                            MatchSoundsLike: missing, MatchAllWordForms: false, Forward: true, Wrap: Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue,
                            Format: false, ReplaceWith: missing, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
            }
            app.ActiveDocument.SaveAs(path /*+ Path.GetExtension(templatePath)*/);
            app.ActiveDocument.Close();
            app.Quit();
        }
    }
}
