using AttributeRouting.Web.Mvc;
using SogetiSkills.UI.SogetiSkillsService;
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
        private readonly ISogetiSkillsService _sogetiSkillsService;

        public ProfileController(ISogetiSkillsService sogetiSkillsService)
        {
            _sogetiSkillsService = sogetiSkillsService;
        }

        [GET("Profile/{username}")]
        public virtual ActionResult Details(string username)
        {
            return View();
        }
    }
}