using SogetiSkills.Core.Managers;
using SogetiSkills.UI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Helpers.Security
{
    public class RequireUserTypeAttribute : AuthorizeAttribute
    {
        private readonly Type[] _allowedUserTypes;

        public RequireUserTypeAttribute(params Type[] allowedUserTypes)
        {
            _allowedUserTypes = allowedUserTypes;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var controller = (SogetiSkillsControllerBase)filterContext.Controller;
            var user = controller.LoggedInUser;
            if (user == null)
            {
                return;
            }
            else if (!_allowedUserTypes.Contains(user.GetType()))
            {
                filterContext.Result = CreateRedirectToRestrictedPage(filterContext);
            }
        }

        private RedirectResult CreateRedirectToRestrictedPage(AuthorizationContext filterContext)
        {
            var urlHelper = new UrlHelper(filterContext.RequestContext);
            string urlToRestrictedPage = urlHelper.Action(MVC.Home.Restricted());
            return new RedirectResult(urlToRestrictedPage);
        }
    }
}