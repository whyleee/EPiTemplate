﻿using System;
using System.Web.Mvc;

namespace EpiTemplate
{
    public class Global : EPiServer.Global
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}