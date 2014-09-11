using SogetiSkills.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SogetiSkills.UI.Helpers.Security
{
    public class AuthCookieHelper : IAuthCookieHelper
    {
        private readonly IUserManager _userManager;

        public AuthCookieHelper(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public void SetAuthCookie(int userId, HttpContextBase httpContext)
        {
            HttpCookie authCookie = FormsAuthentication.GetAuthCookie(userId.ToString(), false);
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