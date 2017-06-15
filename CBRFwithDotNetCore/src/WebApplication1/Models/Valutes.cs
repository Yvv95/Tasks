using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBRFConverter.Models
{
    public class Valutes
    {
        public string Name { get; set; }//Доллар США
        public string Exchange { get; set; }
        public int WorldName { get; set; }//840
        public string ChCode { get; set; }//USD
        public bool IsChecked { get; set; }//надо ли отображать при конвертировании
        public double HowMuch { get; set; }//сколько этой валюты - для конвертировании. если "-1", то не отображается
    }
}

