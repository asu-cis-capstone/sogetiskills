using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    [Authorize]
    public partial class ProfileController : ControllerBase
    {
        [GET("Profile/{username}")]
        public virtual ActionResult Details(string username)
        {
            return View();
        }
    }
}