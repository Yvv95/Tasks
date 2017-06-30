using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CBRFConverter.Models;
using CBRFConverter.ValutesApi;
using CBRFService;

namespace CBRFConverter.Controllers
{
    public class HomeController : Controller
    {
        //отображение отмеченных валют
        public IActionResult Index()
        {
            //проверка на актуальность курса валют
            LoadValutes helper = new LoadValutes();
            string cbrfUpdate = helper.LastLoad();

            if  (HttpContext.Session.GetString("updated") == null)
            {
                HttpContext.Session.SetString("updated", cbrfUpdate);
                ViewBag.LoadDate = cbrfUpdate;
                if (Startup.lastLoadDate != cbrfUpdate)
                {
                    helper.Update();
                    Startup.lastLoadDate = cbrfUpdate;
                }
                foreach (Valutes toAdd in Startup.vals.ValsList)
                {
                    HttpContext.Session.SetString(toAdd.Name.Trim(), false.ToString());
                }
                Dictionary<string, string> bags = new Dictionary<string, string>();
                ViewBag.Vals = bags;
                return View();
            }
            else
                if (HttpContext.Session.GetString("updated") != cbrfUpdate)
            {
                HttpContext.Session.SetString("updated", cbrfUpdate);
                Startup.lastLoadDate = cbrfUpdate;
                helper.Update();
            }
            Dictionary<string, string> toSend = new Dictionary<string, string>();
            foreach (string _name in Startup.vals.getNames())
            {
                string temp = HttpContext.Session.GetString(_name);
                if (HttpContext.Session.GetString(_name) == "True")
                    toSend.Add(_name, Startup.vals.GetExchange(_name));
            }
            ViewBag.LoadDate = cbrfUpdate;
            ViewBag.Vals = toSend;
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
            return View();
        }

        public IActionResult Dynamic()
        {
            ViewBag.SelectList = (List<string>)Startup.vals.getNames();
            return View();
        }
    }
}
