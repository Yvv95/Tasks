using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


//TODO
//Изменить цвет галочек в настройках
//Вылет из-за неправильной проверки даты
//Конвертер при перезагрузке страницы не обнуляет отмеченные
//InvalidOperationException: The communication object, System.ServiceModel.Channels.ServiceChannel, cannot be modified while it is in the Opened state.
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
