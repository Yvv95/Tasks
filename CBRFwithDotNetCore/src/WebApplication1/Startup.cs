using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;
using CBRFService;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.FileProviders;

using CBRFConverter.Models;
using CBRFConverter.ValutesApi;
using CBRFConverter.XmlClasses;
using WebApplication1.ValutesApi;

namespace CBRFConverter
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public static Valutes val;
        public static ValutesFunctions vals;
        public static string lastLoadDate;
        public static Dictionary<string, string> codesList = new Dictionary<string, string>();
        public static ValuteListConverter converter = new ValuteListConverter();

        //public IConfigurationRoot Configuration { get; }
        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("global.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"global.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();
        //    Configuration = builder.Build();
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            ////для БД
            //// получаем строку подключения из файла конфигурации
            //string connection = Configuration.GetConnectionString("DefaultConnection");
            //// добавляем контекст MobileContext в качестве сервиса в приложение
            //services.AddDbContext<EnumValuteContext>(options =>
            //    options.UseSqlServer(connection));

            //подключение сервисов
            services.AddTransient<TimeService>();
            services.AddMvc();
            //сессии
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.CookieName = ".Settings.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(5);
            });
        }
        public class TimeService
        {
            public string GetTime() => System.DateTime.Now.ToString("hh:mm:ss");
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //DBMethods.GetEnumValutes();
            //string connection = Configuration.GetConnectionString("DefaultConnection");

            app.UseDeveloperExceptionPage();
            //сессии
            app.UseSession();
            app.UseStaticFiles();
            loggerFactory.AddConsole();
            #region Страницы
            //чтобы просмотреть список страниц в wwwroot
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),
                RequestPath = new PathString("/pages")
            });

            app.Map("/about", About);
            app.Map("/time", timeShow);
            app.Map("/Home/Settings", settings);
            app.Map("/Home/Dynamic", dynamic);
            app.Map("/Home/Convert", convert);
            #endregion

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/");
            });

            vals = new ValutesFunctions();
            ValuteCollection valXML = new ValuteCollection();
            EnumValuteCollection enumValute = new EnumValuteCollection();
            //загрузка с сервиса
            var loader = new CBRFService.DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap);
            try
            {
                await loader.OpenAsync();
                var lastLoadTask = loader.GetLatestDateTimeAsync(new GetLatestDateTimeRequest());
                var _lastDate = lastLoadTask.Result;
                //var loadValTask = loader.EnumValutesXMLAsync(new EnumValutesXMLRequest());

                lastLoadDate = _lastDate.GetLatestDateTimeResult.ToString(@"dd.MM.yyyy");
                //  var _cursTable = loadValTask.Result;
                //последние курсы
                var _cursTable2 =
                    loader.GetCursOnDateXMLAsync(new GetCursOnDateXMLRequest(_lastDate.GetLatestDateTimeResult)).Result;
                //список валют
                var _cursTable3 = loader.EnumValutesXMLAsync(new EnumValutesXMLRequest(false)).Result;
                string a = _cursTable2.GetCursOnDateXMLResult.ToString().Trim();
                string b = _cursTable3.EnumValutesXMLResult.ToString().Trim();

                using (TextReader _tmp = new StringReader(a))
                {
                    XmlSerializer serializer2 = new XmlSerializer(typeof(ValuteCollection));
                    valXML = (ValuteCollection)
                        serializer2.Deserialize(_tmp);
                }
                using (TextReader _tmp = new StringReader(b))
                {
                    XmlSerializer serializer2 = new XmlSerializer(typeof(EnumValuteCollection));
                    enumValute = (EnumValuteCollection)
                        serializer2.Deserialize(_tmp);
                }

                for (int i = 0; i < enumValute.ValsList.Count(); i++)
                {

                    if (!codesList.ContainsKey(enumValute.ValsList[i].Vname.Trim()) &&
                        (enumValute.ValsList[i].Vname != null) && (enumValute.ValsList[i].Vcode != null))
                        codesList.Add(enumValute.ValsList[i].Vname.Trim(), enumValute.ValsList[i].Vcode.Trim());

                    try
                    {
                        DBMethods.CreateEnumValute(enumValute.ValsList);
                    }
                    catch { };
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                await loader.CloseAsync();
            }

            if (vals != null)
                foreach (var curValue in valXML.ValsList)
                {
                    vals.Create(curValue.Vname.Trim(), curValue.Vcurs.Trim(), Int32.Parse(curValue.Vcode), curValue.VchCode.Trim());
                    converter.AddValues(curValue.Vname.Trim(), curValue.Vcurs.Trim());
                }
        }

        private static void settings(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "settings",
                    template: "{controller=Home}/{action=Settings}/");
            });
        }
        private static void dynamic(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "dynamic",
                    template: "{controller=Home}/{action=Dynamic}/");
            });
        }

        private static void convert(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "convert",
                    template: "{controller=Home}/{action=Convert}/");
            });
        }

        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Простой сервис по отображению курсов валют на основе данных ЦБРФ");
            });
        }
        private static void timeShow(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                TimeService timeService = app.ApplicationServices.GetService<TimeService>();
                await context.Response.WriteAsync($"Current time: {timeService?.GetTime()}");
            });
        }
    }
}
