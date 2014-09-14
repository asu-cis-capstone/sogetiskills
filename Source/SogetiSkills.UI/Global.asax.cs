using FluentValidation;
using FluentValidation.Mvc;
using SogetiSkills.UI.Controllers;
using SogetiSkills.UI.Infrastructure.DependencyResolution;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

namespace SogetiSkills.UI
{
    public class Application : System.Web.HttpApplication
    {
        public static DateTime AssemblyTimestamp;

        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
            AssemblyTimestamp = GetUIAssemblyWriteTime();
        }

        private DateTime GetUIAssemblyWriteTime()
        {
            var uiAssembly = typeof(AccountController).Assembly;
            var assemblyFileInfo = new FileInfo(uiAssembly.Location);
            return assemblyFileInfo.LastWriteTime;
        }
    }
}
