using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CBRFConverter.Models
{
    public class ValuteListConverter
    {

        public double countedSum { get; set; }
        public List<ValuteConverter> lists { get; set; } = new List<ValuteConverter>();
        public string exitVal { get; set; }

        public ObservableCollection<string> allList
        {
            get { return new ObservableCollection<string>(lists.Where(e => e.name.Length > 0).Select(e => e.name)); }
        }

        public ObservableCollection<ValuteConverter> selectedList
        {
            get { return new ObservableCollection<ValuteConverter>(lists.Where(e => e.selected).Select(e => e)); }
        }
        public ObservableCollection<string> listToSelect
        {
            get { return new ObservableCollection<string>(lists.Where(e => !e.selected).Select(e => e.name)); }
        }

        public List<ValuteConverter> getUnChecked()
        {
            List<ValuteConverter> a = new List<ValuteConverter>();
            foreach (ValuteConverter cur in lists)
                if (!cur.selected)
                    a.Add(cur);
            return a;
        }

        public void AddValues(string _name, string _exchange)
        {
            ValuteConverter a = new ValuteConverter(_name, _exchange);
            lists.Add(a);

        }

        public double tryCount()
        {
            countedSum = 0;
            double ratio = 1;
            foreach (ValuteConverter val in lists)
            {
                if (val.selected)
                    countedSum += val.counts * double.Parse(val.exchange.Replace(".", ","));
                if (val.name == exitVal)
                    ratio = double.Parse(val.exchange.Replace(".", ","));
            }
            // CountedSum = countedSum;      
            countedSum = countedSum / ratio;
            return Math.Round(countedSum, 2);
        }


    }
}
