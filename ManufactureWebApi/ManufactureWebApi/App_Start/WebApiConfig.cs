using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ManufactureWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы Web API

            // Маршруты Web API
            config.MapHttpAttributeRoutes();


            //Так как маршрутизация будет применяться с помощью атрибутов
            //стандартный маршрут не потребуется

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });
        }
    }
}
