using AttributeRouting.Web.Mvc;
using SogetiSkills.Core.Managers;
using SogetiSkills.Core.Models;
using SogetiSkills.UI.Helpers.Security;
using SogetiSkills.UI.ViewModels.CanonicalSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    [RequireUserTypeAttribute(typeof(AccountExecutive))]
    public partial class CanonicalSkillController : SogetiSkillsControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ITagManager _tagManager;

        public CanonicalSkillController(IUserManager userManager, ITagManager tagManager)
            : base(userManager)
        {
            _userManager = userManager;
            _tagManager = tagManager;
        }

        [GET("CanonicalSkills/List")]
        public virtual async Task<ActionResult> List()
        {
            var model = await _tagManager.LoadCanonicalTagsAsync();
            return View(model);
        }
        
        [GET("CanonicalSkill/Add")]
        public virtual ActionResult Add()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [POST("CanonicalSkill/Add")]
        public virtual ActionResult Add(AddViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}