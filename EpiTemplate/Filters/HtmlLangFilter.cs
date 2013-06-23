using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpiTemplate.Filters
{
    public class HtmlLangFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                filterContext.Controller.ViewBag.Lang = CultureInfo.CurrentUICulture.Name;
            }
        }
    }
}