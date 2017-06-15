using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CBRFConverter.Models;
using CBRFConverter.ValutesApi;

namespace CBRFConverter.Controllers
{
    public class HomeController : Controller
    {
        //отображение отмеченных валют
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("loaded") == null)
            {
                HttpContext.Session.SetString("loaded", "yes");
                ViewBag.LoadDate = Startup.lastLoadDate;
                foreach (Valutes toAdd in Startup.vals.ValsList)
                {
                    HttpContext.Session.SetString(toAdd.Name.Trim(), false.ToString());
                }
                Dictionary<string, string> bags = new Dictionary<string, string>();
                ViewBag.Vals = bags;

                return View();
            }

            ValutesFunctions toSend = Startup.vals;
            for (int i = 0; i < toSend.ValsList.Count; i++)
            {              
                if (HttpContext.Session.GetString(toSend.ValsList[i].Name.Trim()) != null)        
                    toSend.ValsList[i].IsChecked =
                        Boolean.Parse(HttpContext.Session.GetString(toSend.ValsList[i].Name.Trim()));            
            }
            ViewBag.LoadDate = Startup.lastLoadDate;
            ViewBag.Vals = (Dictionary<string, string>)toSend.getChecked();
            return View();
        }

        //выбор валют для отображения
        public IActionResult Settings()
        {
            ValutesFunctions toSend = Startup.vals;
            for (int i = 0; i < toSend.ValsList.Count; i++)
            {
                if (HttpContext.Session.GetString(toSend.ValsList[i].Name.Trim()) != null)
                {
                    toSend.ValsList[i].IsChecked =
                        Boolean.Parse(HttpContext.Session.GetString(toSend.ValsList[i].Name.Trim()));
                }
            }
            ViewBag.LoadDate = Startup.lastLoadDate;
            ViewBag.Vals = toSend.ValsList;
            return View();
        }
        public IActionResult Convert()
        {
            ViewBag.SelectList = (List<string>)Startup.vals.getNames();
            //отдельный класс для конвертора с дублированием полей валют?

            return View();
        }

        public IActionResult Dynamic()
        {
            ViewBag.SelectList = (List<string>)Startup.vals.getNames();

            return View();
        }
    }
}
