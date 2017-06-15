using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CBRFConverter
{
    //список валют с дополнительными полями
    public class EnumValutes
    {
        public string Vcode { get; set; }//поле для загрузки динамики
        public string Vname { get; set; }
        public string VEngname { get; set; }
        public string Vnom { get; set; }
        public string VcommonCode { get; set; }
        public string VnumCode { get; set; }
        public string VcharCode { get; set; }
    }

    [XmlRoot("ValuteData")]
    public class EnumValuteCollection
    {
        [XmlElement("EnumValutes")]
        public EnumValutes[] ValsList { get; set; }
    }
//ValuteData xmlns="">
  //< EnumValutes >
    //< Vcode > R01010 </ Vcode >
    //< Vname > Австралийский доллар</Vname>
    //<VEngname>Australian Dollar</VEngname>
    //<Vnom>1</Vnom>
    //<VcommonCode>R01010</VcommonCode>
    //<VnumCode>36</VnumCode>
    //<VcharCode>AUD</VcharCode>
  //</EnumValutes>

  //<EnumValutes>
    //<Vcode>R01015</Vcode>
    //<Vname>Австрийский шиллинг </Vname>
    //<VEngname>Austrian Shilling </VEngname>
    //<Vnom>1000</Vnom>
    //<VcommonCode>R01015</VcommonCode>
    //<VnumCode>40</VnumCode>
    //<VcharCode>ATS</VcharCode>
  //</EnumValutes>
}


