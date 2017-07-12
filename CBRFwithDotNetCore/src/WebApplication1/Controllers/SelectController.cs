using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CBRFService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CBRFConverter.Models;
using CBRFConverter.XmlClasses;
using CBRFConverter.ValutesApi;
using WebApplication1.ValutesApi;

namespace CBRFConverter.Controllers
{
    public class SelectController : Controller
    {
        [HttpPost]
        public ActionResult LoadCurses(string valName, string period)
        {
            //здесь не проверяем на обновления, т.к. всё равно приходиться подгружать с помощью других методов

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
            DynamicValuteCollection dynamicVals = new DynamicValuteCollection();
            List<DayCursePairs> pointList = new List<DayCursePairs>();

            pointList = DBMethods.LoadCurses(valName, DateTime.Now.AddDays(-daysMinus), DateTime.Now);
            if (pointList.Count < daysMinus/2)
            {
                var loader = new CBRFService.DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap);
                pointList = new List<DayCursePairs>();
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
                    for (int i = 0; i < dynamicVals.ValsList.Count(); i++)
                    {
                        DateTime tmp = DateTime.Parse(dynamicVals.ValsList[i].CursDate);
                        dynamicVals.ValsList[i].CursDate = tmp.ToString(@"dd.MM.yyyy");
                        pointList.Add(new DayCursePairs(dynamicVals.ValsList[i].CursDate, dynamicVals.ValsList[i].Vcurs));
                    }
                    DBMethods.AddCurse(valName, pointList);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    loader.CloseAsync();
                }
            }

            return new JsonResult(pointList);
        }
    }
}
