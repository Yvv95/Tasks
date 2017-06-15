using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CBRFConverter.XmlClasses
{
    //изменение круса вылюты
    public class ValuteCursDynamic
    {
        public string CursDate { get; set; }
        public string Vcode { get; set; }
        public string Vnom { get; set; }
        public string Vcurs { get; set; }
    }
    [XmlRoot("ValuteData")]
    public class DynamicValuteCollection
    {
        [XmlElement("ValuteCursDynamic")]
        public ValuteCursDynamic[] ValsList { get; set; }
    }
}
//<ValuteData xmlns = "" >
 //<ValuteCursDynamic>
  //<CursDate > 2017 - 05 - 23T00:00:00+03:00</CursDate>
  //<Vcode>R01010</Vcode>
  //<Vnom>1</Vnom>
  //<Vcurs>42.0973</Vcurs>
 //</ValuteCursDynamic>

 //<ValuteCursDynamic>
  //<CursDate>2017-05-24T00:00:00+03:00</CursDate>
  //<Vcode>R01010</Vcode>
  //<Vnom>1</Vnom>
  //<Vcurs>42.4221</Vcurs>
 //</ValuteCursDynamic>