using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SogetiSkills.UI.Infrastructure.Security
{
    public class Authentication : IAuthentication
    {
        private string[] users = new[] { "jwathen", "osin", "jmonje", "jmoran" };

        public Task<bool> ValidateUsernamePasswordAsync(string username, string password)
        {
            bool valid = username.Contains(username) && username == password;
            return Task.FromResult(valid);
        }

        public void SetAuthCookie(string username, HttpContextBase httpContext)
        {
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(username, false);
            httpContext.Response.Cookies.Add(authCookie);
        }

        public void ClearAuthCookie(HttpContextBase httpContext)
        {
            HttpCookie authCookie = httpContext.Response.Cookies[FormsAuthentication.FormsCookieName];
            authCookie.Expires = DateTime.Today.AddDays(-1);

            httpContext.Session.RemoveAll();
        }
    }
}