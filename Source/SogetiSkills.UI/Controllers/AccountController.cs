using AttributeRouting.Web.Mvc;
using SogetiSkills.UI.Infrastructure.Security;
using SogetiSkills.UI.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFlashMessages;
using System.Threading.Tasks;

namespace SogetiSkills.UI.Controllers
{
    public partial class AccountController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AccountController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [GET("Account/SignIn")]
        public virtual ActionResult SignIn()
        {
            if (Request.IsAuthenticated)
            {
                _authentication.ClearAuthCookie(HttpContext);
            }
            return View();
        }

        [POST("Account/SignIn")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _authentication.ValidateUsernamePasswordAsync(model.Username, model.Password))
            {
                _authentication.SetAuthCookie(model.Username, HttpContext);
                return RedirectToAction(MVC.Home.Index());
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
                return View(model);
            }
        }

        [GET("Account/SignOut")]
        public virtual ActionResult SignOut()
        {
            _authentication.ClearAuthCookie(HttpContext);
            FlashInfo("You have been signed out.");
            return RedirectToAction(MVC.Account.SignIn());
        }
    }
}