using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ConvertController : Controller
    {
        // GET: /<controller>/
        [HttpPost]
        public ActionResult AddToConvert(string name)
        {
            for (int i = 0; i < Startup.converter.lists.Count(); i++)
            {
                if (Startup.converter.lists[i].name.Trim() == name.Trim())
                {
                    Startup.converter.lists[i].selected = true;
                    return new JsonResult(Startup.converter.getUnChecked());
                }
            }
            return Json(new { resultMessage = false });
        }

        [HttpPost]
        public ActionResult RemoveFromConvert(string name)
        {
            for (int i = 0; i < Startup.converter.lists.Count(); i++)
            {
                if (Startup.converter.lists[i].name.Trim() == name.Trim())
                {
                    Startup.converter.lists[i].selected = false;
                    return new JsonResult(Startup.converter.getUnChecked());
                }
            }
            return Json(new { resultMessage = false });
        }

        public ActionResult LoadList()
        {
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

        [HttpPost]
        public ActionResult TryCount(List<string> _valList, List<string> _numList, string _exitval)
        {
            bool parsed = false;
            Startup.converter.exitVal = _exitval;
            for (int i = 0; i < _valList.Count; i++)
            {
                int temp = Startup.converter.allList.IndexOf(_valList[i]);
                if (temp > -1)
                {
                    double parseRes = 0;
                    parsed = double.TryParse(_numList[i].Replace(".", ","), out parseRes);
                    if (parsed == false)
                        return Json(new { countedSum = 0, failVal = _valList[i] });
                    Startup.converter.lists[temp].counts = parseRes;
                }
            }
            double sum =
                Startup.converter.tryCount();
            for (int i = 0; i < Startup.converter.lists.Count; i++)
                Startup.converter.lists[i].counts = 0;
            return Json(new { counted = sum, failVal = "ok" });
        }
    }
}
