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
    public partial class ConsultantController : ControllerBase
    {
        [GET("Consultant/{id}")]
        public virtual ActionResult Details(int id)
        {
            return View();
        }
    }
}