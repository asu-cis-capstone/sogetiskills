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
        private readonly ISkillManager _skillManager;

        public CanonicalSkillController(IUserManager userManager, ISkillManager skillManager)
            : base(userManager)
        {
            _userManager = userManager;
            _skillManager = skillManager;
        }

        [GET("CanonicalSkills/List")]
        public virtual async Task<ActionResult> List()
        {
            var model = await _skillManager.LoadCanonicalSkillsAsync();
            return View(model);
        }
        
        [GET("CanonicalSkill/Add")]
        public virtual ActionResult Add()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [POST("CanonicalSkill/Add")]
        public virtual async Task<ActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _skillManager.AddCanonicalSkillAsync(model.Name, model.Description);
            FlashSuccess(string.Format("{0} was added.", model.Name));
            return RedirectToAction(MVC.CanonicalSkill.List());
        }

        [GET("CanonicalSkill/Edit/{id}")]
        public virtual async Task<ActionResult> Edit(int id)
        {
            var skill = await _skillManager.LoadByIdAsync(id);
            if (skill == null)
            {
                return HttpNotFound();
            }

            EditViewModel model = new EditViewModel();
            model.Id = skill.Id;
            model.Name = skill.Name;
            model.Description = skill.Description;

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [POST("CanonicalSkill/Edit/{id}")]
        public virtual async Task<ActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _skillManager.UpdateSkillAsync(model.Id, model.Name, model.Description, true);
            FlashSuccess(string.Format("{0} updated.", model.Name));
            return RedirectToAction(MVC.CanonicalSkill.List());
        }

        [ValidateAntiForgeryToken]
        [POST("CanonicalSkill/Delete")]
        public virtual async Task<ActionResult> Delete(int skillId)
        {
            var skill = await _skillManager.LoadByIdAsync(skillId);
            if (skill != null)
            {
                await _skillManager.RemoveCanonicalSkillAsync(skillId);
                FlashSuccess(string.Format("{0} was removed.", skill.Name));
            }
            return RedirectToAction(MVC.CanonicalSkill.List());
        }
    }
}