using AttributeRouting.Web.Mvc;
using MvcFlashMessages;
using SogetiSkills.Managers;
using SogetiSkills.Models;
using SogetiSkills.UI.Infrastructure.Security;
using SogetiSkills.UI.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    public partial class AccountController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        private readonly IUserManager _userManager;

        public AccountController(IAuthentication authentication, IUserManager userManager)
        {
            _authentication = authentication;
            _userManager = userManager;
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

            if (await _authentication.ValidateEmailAddressAndPasswordAsync(model.EmailAddress, model.Password))
            {
                _authentication.SetAuthCookie(model.EmailAddress, HttpContext);
                return RedirectToAction(MVC.Home.Index());
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email address or password is incorrect.");
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

        [GET("Account/Register")]
        public virtual ActionResult Register()
        {
            return View();
        }

        [POST("Account/Register")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _userManager.RegisterNewUserAsync<Consultant>(model.EmailAddress, model.Password, model.FirstName, model.LastName);
            _authentication.SetAuthCookie(model.EmailAddress, HttpContext);
            return RedirectToAction(MVC.Home.Index());
        }
    }
}