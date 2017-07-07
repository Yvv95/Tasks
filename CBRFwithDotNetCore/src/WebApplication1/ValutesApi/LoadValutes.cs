using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBRFService;
using System.IO;
using System.Xml.Serialization;
namespace CBRFConverter.ValutesApi
{
    public class LoadValutes
    {
        //загрузка даты курсов
        public string LastLoad()
        {
            string lastLoadDate = "";
            var loader = new CBRFService.DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap);
            try
            {
                loader.OpenAsync();
                var lastLoadTask = loader.GetLatestDateTimeAsync(new GetLatestDateTimeRequest());
                var _lastDate = lastLoadTask.Result;
                lastLoadDate = _lastDate.GetLatestDateTimeResult.ToString(@"dd.MM.yyyy");
            }
            finally
            {
                loader.CloseAsync();
            }
            return lastLoadDate;
        }

        public void Update()
        {

            //обновляем Startup
            var loader = new CBRFService.DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap);
            try
            {
                loader.OpenAsync();
                var lastLoadTask = loader.GetLatestDateTimeAsync(new GetLatestDateTimeRequest());
                var _lastDate = lastLoadTask.Result;
                //последние курсы
                var _cursTable2 =
                    loader.GetCursOnDateXMLAsync(new GetCursOnDateXMLRequest(_lastDate.GetLatestDateTimeResult)).Result;
                string a = _cursTable2.GetCursOnDateXMLResult.ToString();
                ValuteCollection valXML = new ValuteCollection();

                using (TextReader _tmp = new StringReader(a))
                {
                    XmlSerializer serializer2 = new XmlSerializer(typeof(ValuteCollection));
                    valXML = (ValuteCollection)
                        serializer2.Deserialize(_tmp);
                }
                foreach (var curValue in valXML.ValsList)
                {
                    Startup.vals.Update(curValue.Vname.Trim(), curValue.Vcurs.Trim());
                    Startup.converter.UpdateValute(curValue.Vname.Trim(), curValue.Vcurs.Trim());
                }
            }
            finally
            {
                loader.CloseAsync();
            }
        }

        //сделать метод с обновлением курсов валют

    }
}

