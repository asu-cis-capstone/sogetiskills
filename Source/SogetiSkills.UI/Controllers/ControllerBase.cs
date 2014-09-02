using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFlashMessages;
using SogetiSkills.UI.SogetiSkillsService;

namespace SogetiSkills.UI.Controllers
{
    public abstract class ControllerBase : Controller
    {
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