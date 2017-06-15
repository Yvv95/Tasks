using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBRFConverter.Models
{
    public class DayCursePairs
    {

        public string Day { get; set; }
        public string Curse { get; set; }

        public DayCursePairs(string _day, string _curse)
        {
            Day = _day;
            Curse = _curse;
        }
    }
}
