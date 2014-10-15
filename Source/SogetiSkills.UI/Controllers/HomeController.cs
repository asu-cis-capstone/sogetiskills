using AttributeRouting.Web.Mvc;
using SogetiSkills.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    public partial class HomeController : SogetiSkillsControllerBase
    {
        public HomeController(IUserManager userManager)
            : base(userManager)
        {

        }

        [GET("/")]
        public virtual ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction(MVC.Profile.Details(LoggedInUserId.Value));
            }
            else
            {
                return RedirectToAction(MVC.Account.SignIn());
            }
        }

        [GET("/restricted")]
        public virtual ActionResult Restricted()
        {
            return View();
        }

        [GET("/error")]
        public virtual ActionResult Error()
        {
            return View();
        }

        [GET("/notfound")]
        public virtual ActionResult NotFound()
        {
            return View();
        }

        [AttributeRouting.Web.Mvc.Route("MainNavigation", HttpVerbs.Get, HttpVerbs.Post)]
        [ChildActionOnly]
        public virtual ActionResult MainNavigation()
        {
            var user = LoggedInUser;
            return View(user);
        }
    }
}