using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    [Authorize]
    public partial class HomeController : Controller
    {
        [GET("/")]
        public virtual ActionResult Index()
        {
            return View();
        }

        [GET("/restricted")]
        public virtual ActionResult Restricted()
        {
            return View();
        }
    }
}