using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EPiTemplate.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // NOTE
            // ====
            // 
            // Default ASP.NET MVC routes are commented, because EPiServer uses its own route
            // which will conflict with default ASP.NET MVC route.
            //
            // Do not uncomment them, or you could face unexpected behavior in EPiServer.
            // To register a custom controller, use "%ControllerName%/{action}/{id}" pattern.

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            //);
        }
    }
}