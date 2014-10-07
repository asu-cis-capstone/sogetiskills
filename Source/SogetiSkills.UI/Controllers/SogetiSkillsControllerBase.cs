using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFlashMessages;
using SogetiSkills.UI.Helpers.Security;
using SogetiSkills.Core.Managers;
using System.Threading.Tasks;
using SogetiSkills.Core.Models;

namespace SogetiSkills.UI.Controllers
{
    public abstract class SogetiSkillsControllerBase : Controller
    {
        private readonly IUserManager _userManager;
        private User _loggedInUser = null;

        public SogetiSkillsControllerBase()
        {

        }

        public SogetiSkillsControllerBase(IUserManager userManager)
        {
            _userManager = userManager;
        }

        protected int? LoggedInUserId
        {
            get
            {
                if (!Request.IsAuthenticated)
                {
                    return null;
                }
                return int.Parse(HttpContext.User.Identity.Name);
            }
        }

        public User LoggedInUser
        {
            get
            {
                if (!Request.IsAuthenticated)
                {
                    return null;
                }

                if (_loggedInUser == null)
                {
                    int userId = LoggedInUserId.Value;
                    _loggedInUser = _userManager.LoadUserById(userId);
                }

                return _loggedInUser;
            }
        }

        public void FlashSuccess(string message)
        {
            this.Flash("success", message);
        }

        public void FlashInfo(string message)
        {
            this.Flash("info", message);
        }

        public void FlashError(string message)
        {
            this.Flash("error", message);
        }
    }
}