using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SogetiSkills.UI.Infrastructure.Security
{
    public interface IAuthentication
    {
        Task<bool> ValidateUsernamePasswordAsync(string username, string password);
        void SetAuthCookie(string username, HttpContextBase httpContext);
        void ClearAuthCookie(HttpContextBase httpContext);
    }
}
