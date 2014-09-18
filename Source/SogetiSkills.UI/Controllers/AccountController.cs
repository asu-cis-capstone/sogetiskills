using AttributeRouting.Web.Mvc;
using MvcFlashMessages;
using SogetiSkills.Managers;
using SogetiSkills.Models;
using SogetiSkills.UI.Helpers.Security;
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
        private readonly IAuthCookieHelper _authCookieHelper;
        private readonly IUserManager _userManager;

        public AccountController(IAuthCookieHelper authCookieHelper, IUserManager userManager)
        {
            _authCookieHelper = authCookieHelper;
            _userManager = userManager;
        }

        [GET("Account/SignIn")]
        public virtual ActionResult SignIn()
        {
            if (Request.IsAuthenticated)
            {
                _authCookieHelper.ClearAuthCookie(HttpContext);
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

            var user = await _userManager.ValidatePasswordAsync(model.EmailAddress, model.Password);
            if (user != null)
            {
                _authCookieHelper.SetAuthCookie(user.Id, HttpContext);
                return RedirectToAction(MVC.Profile.Details(user.Id));
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
            _authCookieHelper.ClearAuthCookie(HttpContext);
            FlashInfo("You have been signed out.");
            return RedirectToAction(MVC.Account.SignIn());
        }

        [GET("Account/Register")]
        public virtual ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [POST("Account/Register")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = null;
            if (model.AccountType == AccountTypes.CONSULTANT)
            {
                user = await _userManager.RegisterNewUserAsync<Consultant>(model.EmailAddress, model.Password, model.FirstName, model.LastName, model.PhoneNumber);
            }
            else if (model.AccountType == AccountTypes.ACCOUNT_EXECUTIVE)
            {
                user = await _userManager.RegisterNewUserAsync<AccountExecutive>(model.EmailAddress, model.Password, model.FirstName, model.LastName, model.PhoneNumber);
            }

            _authCookieHelper.SetAuthCookie(user.Id, HttpContext);
            return RedirectToAction(MVC.Profile.Details(user.Id));
        }
    }
}