using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspnetMvcDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Authentication", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PagesDefault",
                url: "Pages/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional },
                namespaces: new[] { "AspnetMvcDemo.Controllers.Pages" }
            ).DataTokens["UseNamespaceFallback"] = false;

            routes.MapRoute(
               name: "BreakConcreteSevenDays",
               url: "Pages/{controller}/{action}/{id}/{duration}",
               defaults: new { controller = "TestingResults", action = "ConcreteTestingResult", id = "", duration = "" }
            );
        }
    }
}
