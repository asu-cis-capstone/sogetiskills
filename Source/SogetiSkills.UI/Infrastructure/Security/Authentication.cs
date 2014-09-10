using SogetiSkills.Managers;
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
        private readonly IUserManager _userManager;

        public Authentication(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ValidateEmailAddressAndPasswordAsync(string emailAddress, string password)
        {
            return await _userManager.ValidatePasswordAsync(emailAddress, password);
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