using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    public class ConsultantController : SogetiSkillsControllerBase
    {
        [GET("Consultants")]
        public ActionResult Index()
        {
            return View();
        }
    }
}