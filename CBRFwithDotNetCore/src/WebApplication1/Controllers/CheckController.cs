using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CBRFConverter.Models;

namespace CBRFConverter.Controllers
{
    public class CheckController : Controller
    {

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
            //если не существует, то заполняем сессию значениями false
            foreach (Valutes _valute in Startup.vals.ValsList)
            {
                HttpContext.Session.SetString(_valute.Name.Trim(), false.ToString());
            }          
            return Json(new {resultMessage = "Не удалось найти валюту " + name});
        }

    }
}
