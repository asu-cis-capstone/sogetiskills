using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SogetiSkills.UI.Helpers.Security
{
    public interface IAuthCookieHelper
    {
        void SetAuthCookie(int userId, HttpContextBase httpContext);
        void ClearAuthCookie(HttpContextBase httpContext);
    }
}
