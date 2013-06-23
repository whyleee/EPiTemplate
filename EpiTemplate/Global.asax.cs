using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EpiTemplate.App_Start;

namespace EpiTemplate
{
    public class Global : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}