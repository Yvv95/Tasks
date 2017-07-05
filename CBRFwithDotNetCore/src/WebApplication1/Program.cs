using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


//TODO
//Сделать переноы на 1й странице
//Изменить цвет галочек в настройках
//Конвертер при перезагрузке страницы не обнуляет отмеченные
//Выгрузка в БД
namespace CBRFConverter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
               //.UseWebRoot("static")// установка папки static
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
