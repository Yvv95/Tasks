using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CBRFConverter.Models;
using CBRFConverter.ValutesApi;
using CBRFService;
using System.Globalization;

namespace CBRFConverter.Controllers
{
    public class ConvertController : Controller
    {
        //сразу выводить уже отмеченные
        [HttpPost]
        public ActionResult AddToConvert(string name)
        {
            List<ValuteConverter> tmp = new List<ValuteConverter>();
            for (int i = 0; i < Startup.converter.lists.Count(); i++)
            {
                if (HttpContext.Session.GetString("!" + Startup.converter.lists[i].name.Trim()) == "false")
                    tmp.Add(new ValuteConverter(Startup.converter.lists[i].name.Trim()));

                if (Startup.converter.lists[i].name.Trim() == name.Trim())
                {
                    tmp.RemoveAt(tmp.Count - 1);
                    HttpContext.Session.SetString("!" + name.Trim(), "true");
                }
            }
            return new JsonResult(tmp);
            //return Json(new { resultMessage = false });
        }

        [HttpPost]
        public ActionResult RemoveFromConvert(string name)
        {
            for (int i = 0; i < Startup.converter.lists.Count(); i++)
            {
                if (Startup.converter.lists[i].name.Trim() == name.Trim())
                {
                    HttpContext.Session.SetString("!" + Startup.converter.lists[i].name.Trim(), "false");
                    return new JsonResult(Startup.converter.getUnChecked());
                }
            }
            return Json(new { resultMessage = false });
        }

        public ActionResult LoadList()
        {
            for (int i = 0; i < Startup.converter.lists.Count(); i++)
            {
                Startup.converter.lists[i].selected = false;
                HttpContext.Session.SetString("!" + Startup.converter.lists[i].name.Trim(), "false");
            }
            return Json(new { Startup.converter.listToSelect });
        }

        [HttpPost]
        public ActionResult Uncheck(string name)
        {
            for (int i = 0; i < Startup.converter.lists.Count(); i++)
            {
                if (Startup.converter.lists[i].name.Trim() == name.Trim())
                {
                    Startup.converter.lists[i].selected = false;
                    Startup.converter.lists[i].counts = 0f;
                    return new JsonResult(Startup.converter.getUnChecked());
                }
            }
            return Json(new { resultMessage = false });
        }

        //избавиться от хранения в Startup.vals и  Startup.converter
        [HttpPost]
        public ActionResult TryCount(List<string> _valList, List<string> _numList, string _exitval)
        {
            //обновление валют
            LoadValutes helper = new LoadValutes();
            string cbrfUpdate = helper.LastLoad();
            if (HttpContext.Session.GetString("updated") != cbrfUpdate)
            {
                HttpContext.Session.SetString("updated", cbrfUpdate);
                Startup.lastLoadDate = cbrfUpdate;
                helper.Update();
            }
            double sum = 0;
            sum = tryCount(_valList, _numList, _exitval);
            return Json(new { counted = sum, failVal = "ok" });
        }


        private double tryCount(List<string> _valList, List<string> _numList, string _exitval)
        {
            double countedSum = 0, ratio = 1, _count=0, _exch=0;
            for (int i = 0; i < _valList.Count; i++)
            {
                if (double.TryParse(_numList[i].Replace(".", ","), out _count))
                    if (double.TryParse(Startup.vals.GetExchange(_valList[i]).Replace(".", ","), out _exch))
                        countedSum+= _exch* _count;                
            }
            for (int i = 0; i < Startup.vals.ValsList.Count; i++)
                if (Startup.vals.ValsList[i].Name == _exitval)
                      double.TryParse(Startup.vals.ValsList[i].Exchange, NumberStyles.Number, CultureInfo.InvariantCulture, out ratio);

            countedSum = countedSum / ratio;
            return Math.Round(countedSum, 2);
        }
    }
}
