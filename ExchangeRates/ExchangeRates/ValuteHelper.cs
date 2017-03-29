using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRates
{
    public static class ValuteHelper
    {
        public static Dictionary<int, bool> selectedValute = new Dictionary<int, bool>();//для настроек - какие валюты отображать
        public static Dictionary<int, Valutes> ValList = new Dictionary<int, Valutes>();//ключ - название валюты
        
        public static List<Valutes> getChecked()
        {
            List<Valutes> smth = new List<Valutes>();
            foreach (Valutes one in ValList.Values)
            {
                smth.Add(one);
            }

            foreach (Valutes val in smth)
            {
                if (!(selectedValute[val.WorldName]))
                {
                    smth.Remove(val);
                }
            }
            return smth;
        }

        public static List<string> getNames() //получение строкового списка валют
        {
            List<string> _tmp = new List<string>();
            foreach (Valutes one in ValList.Values)
            {
                _tmp.Add(one.Name);
            }
            return _tmp;
        }
        public static Valutes getValuteByName(string nameValute)//получение объекта по названию
        {
            if (nameValute!=null)
            foreach (Valutes two in ValList.Values)
            {
                if (two.Name == nameValute.TrimEnd())
                {
                    return two;
                }
            }
            return new Valutes("","", -1);
        }
        public static double todayPrice(int valueCode)//получение цены валюты в рублях по коду
        {
            try
            {
                return Convert.ToDouble(ValList[valueCode].Exchange);
            }
            catch
            {
                return -1;
            }
        }

        public static double toRouble(int valuteCode, double howMuch)//получение заданного кол-ва заданной валюты в рублях
        {
            double counted = -1;
            if (howMuch>0)
            counted = todayPrice(valuteCode) * howMuch;
            if (counted > 0)
                return counted;
            else
                return 0;
        }         

    }
}
