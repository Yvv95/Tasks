using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

//For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    public class CheckController : Controller
    {
        // GET: /<controller>/
        //public IActionResult Check()
        //{

        //    return View();
        //}
        [HttpPost]
        public ActionResult ChangeCheckbox(string name)
        {
            //смотрим, есть ли заданная валюта
            string _value = HttpContext.Session.GetString(name.Trim());
            if (_value != null)
            {
                bool _newValue = bool.Parse(_value);
                bool _result = !_newValue;
                HttpContext.Session.SetString(name.Trim(), _result.ToString());
                return Json(new {resultMessage = "Значение " + name + " изменено на " + _result});
            }
            //если не существует, то заполняем сессию фолсами
            foreach (Valutes _valute in Startup.vals.ValsList)
            {
                HttpContext.Session.SetString(_valute.Name.Trim(), false.ToString());
            }
            //}
            return Json(new {resultMessage = "Не удалось найти валюту " + name});
        }

    }
}
