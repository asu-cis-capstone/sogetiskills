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
            MvcHandler.DisableMvcResponseHeader = true;
        }

        private DateTime GetUIAssemblyWriteTime()
        {
            var uiAssembly = typeof(AccountController).Assembly;
            var assemblyFileInfo = new FileInfo(uiAssembly.Location);
            return assemblyFileInfo.LastWriteTime;
        }

        protected void Application_BeginRequest()
        {
            if (!Request.IsSecureConnection && Request.RequestType == "GET")
            {
                UriBuilder httpsUriBuilder = new UriBuilder(Request.Url)
                {
                    Scheme = "https",
                    Port = -1 // -1 means the scheme's default port
                };
                string httpsUrl = httpsUriBuilder.Uri.ToString();
                Response.Redirect(httpsUrl);
                Response.Flush();
                Response.End();
            }     
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-Frame-Options");

            Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
            Response.Headers.Add("X-Frame-Options", "DENY");

            foreach(var cookieName in Response.Cookies.AllKeys.Where(x => x != "theme")) // the theme cookie is set in js for now
            {
                Response.Cookies[cookieName].HttpOnly = true;
            }
        }
    }
}
