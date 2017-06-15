using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CBRFConverter
{
    //курс валют на последнюю загрузку
    public class ValuteCursOnDate
    {
        public string Vname { get; set; }
        public string Vnom { get; set; }
        public string Vcurs { get; set; }
        public string Vcode { get; set; }
        public string VchCode { get; set; }
    }

    [XmlRoot("ValuteData")]
    public class ValuteCollection
    {
        [XmlElement("ValuteCursOnDate")]       
        public ValuteCursOnDate[] ValsList { get; set; }
    }


//<ValuteData xmlns = "" OnDate="20170527">  
    //<ValuteCursOnDate>
      //<Vname>Австралийский доллар</Vname>
      //<Vnom>1</Vnom>
      //<Vcurs>42.2208</Vcurs>
      //<Vcode>36</Vcode>
      //<VchCode>AUD</VchCode>
   //</ValuteCursOnDate>

   //<ValuteCursOnDate>
    //<Vname>Азербайджанский манат </Vname>
    //<Vnom>1</Vnom>
    //<Vcurs>33.5834</Vcurs>
    //<Vcode>944</Vcode>
    //<VchCode>AZN</VchCode>
  //</ValuteCursOnDate>

//</ValuteData>

}
