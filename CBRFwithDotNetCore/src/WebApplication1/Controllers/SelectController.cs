using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CBRFService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using CBRFConverter.Models;
using CBRFConverter.XmlClasses;

namespace CBRFConverter.Controllers
{
    public class SelectController : Controller
    {
        [HttpPost]
        public ActionResult LoadCurses(string valName, string period)
        {
            int daysMinus = 7;
            switch (period)
            {
                case "Неделя":
                    daysMinus = 7;
                    break;
                case "Месяц":
                    daysMinus = 31;
                    break;
                case "Квартал":
                    daysMinus = 92;
                    break;
                case "Год":
                    daysMinus = 365;
                    break;
            }
            var loader = new CBRFService.DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap);
            DynamicValuteCollection dynamicVals = new DynamicValuteCollection();

            try
            {
                loader.OpenAsync();
                var _cursTable4 =
                    loader.GetCursDynamicXMLAsync(new GetCursDynamicXMLRequest(DateTime.Now.AddDays(-daysMinus), DateTime.Now,
                        Startup.codesList[valName])).Result;
                string c = _cursTable4.GetCursDynamicXMLResult.ToString();
                using (TextReader _tmp = new StringReader(c))
                {
                    XmlSerializer serializer2 = new XmlSerializer(typeof(DynamicValuteCollection));
                    dynamicVals = (DynamicValuteCollection)
                        serializer2.Deserialize(_tmp);
                }
                loader.CloseAsync();
            }
            catch(Exception e)
            {Console.WriteLine(e); }
            List <DayCursePairs> pointList = new List<DayCursePairs>();

            //for (int i = dynamicVals.ValsList.Count()-1; i >=0 ; i--)
            for (int i=0; i<dynamicVals.ValsList.Count(); i++)
            {
                DateTime tmp = DateTime.Parse(dynamicVals.ValsList[i].CursDate);
                dynamicVals.ValsList[i].CursDate = tmp.ToString(@"dd.MM.yyyy");
                pointList.Add(new DayCursePairs(dynamicVals.ValsList[i].CursDate, dynamicVals.ValsList[i].Vcurs));
            }
            return new JsonResult(pointList);
        }
    }
}
