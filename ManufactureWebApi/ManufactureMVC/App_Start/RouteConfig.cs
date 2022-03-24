using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ManufactureMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //https://localhost:44390/Contact
            routes.MapRoute(
                name: "Contact/{*pathInfo}", 
                url: "Contact",
                defaults: new { controller = "Home", action = "Contact" });

            //https://localhost:44390/About
            routes.MapRoute(
                name: "About",
                url: "About/{*pathInfo}",
                defaults: new { controller = "Home", action = "About" });

            //При отсутствии подходящих маршрутов будет применяться стандартный,
            //содержащий в URL контроллер, которому принадлежат данные действия
            //https://localhost:44390/Contact
            //https://localhost:44390/Home/About
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
