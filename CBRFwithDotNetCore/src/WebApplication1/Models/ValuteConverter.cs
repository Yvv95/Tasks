using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBRFConverter.Models
{
    public class ValuteConverter
    {
        public string name { get; set; }//Доллар США
        public string exchange { get; set; }//55,55
        public double counts { get; set; }//сколько долларов
        public bool selected { get; set; }//добавлено ли на панель перевода

        public ValuteConverter(string _name, string _exchange)
        {
            name = _name;
            exchange = _exchange;
            counts = 0;
            selected = false;
        }
        //чтобы передавать в отметках в конвертере
        public ValuteConverter(string _name)
        {
            name = _name;
        }

        public void updateExchange(string _exchange)
        {
            exchange = _exchange;
        }


    }
}
