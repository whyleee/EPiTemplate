using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace EPiTemplate.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            // jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery",
                cdnPath: "//ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js").Include(
                "~/js/jquery/jquery-{version}.js"
            ));

            // js
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/js/jquery/jquery.unobtrusive*",
                "~/js/jquery/jquery.validate*"
                // Your scripts here...
            ));
            bundles.Add(new ScriptBundle("~/bundles/js/ext").Include(
                "~/js/ext/html5shiv.js",
                "~/js/ext/respond.js"
            ));

            // css
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                // Your css here...
            ));
        }
    }
}