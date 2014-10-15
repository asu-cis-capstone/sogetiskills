using AttributeRouting.Web.Mvc;
using SogetiSkills.Core.Managers;
using SogetiSkills.Core.Models;
using SogetiSkills.UI.ViewModels.Profile;
using SogetiSkills.UI.ViewModels.Profile.Details;
using SogetiSkills.UI.ViewModels.Profile.EditContactInfo;
using SogetiSkills.UI.ViewModels.Profile.Skills;
using SogetiSkills.UI.ViewModels.Profile.UploadResume;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SogetiSkills.UI.Controllers
{
    [Authorize]
    public partial class ProfileController : SogetiSkillsControllerBase
    {
        private readonly IDetailsViewModelBuilder _detailsViewModelBuilder;
        private readonly IEditContactInfoViewModelBuilder _editContactInfoViewModelBuilder;
        private readonly ISkillsViewModelBuilder _skillsViewModelBuilder;
        private readonly IUserManager _userManager;
        private readonly IResumeManager _resumeManager;
        private readonly ISkillManager _skillManager;

        public ProfileController(
            IDetailsViewModelBuilder detailsViewModelBuilder,
            IEditContactInfoViewModelBuilder editContactInfoViewModelBuilder,
            ISkillsViewModelBuilder skillsViewModelBuilder,
            IUserManager userManager,
            IResumeManager resumeManager,
            ISkillManager skillManager)
            : base(userManager)
        {
            _detailsViewModelBuilder = detailsViewModelBuilder;
            _editContactInfoViewModelBuilder = editContactInfoViewModelBuilder;
            _skillsViewModelBuilder = skillsViewModelBuilder;
            _userManager = userManager;
            _resumeManager = resumeManager;
            _skillManager = skillManager;
        }

        [GET("Profile/{UserId}")]
        public virtual async Task<ActionResult> Details(int userId)
        {
            DetailsViewModel model = await _detailsViewModelBuilder.BuildAsync(userId, LoggedInUserId.Value);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [GET("Profile/EditContactInfo/{UserId}")]
        public virtual async Task<ActionResult> EditContactInfo(int userId)
        {
            if (userId != LoggedInUserId)
            {
                return RedirectToAction(MVC.Home.Restricted());
            }

            EditContactInfoViewModel model = await _editContactInfoViewModelBuilder.BuildAsync(userId);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [POST("Profile/EditContactInfo/{UserId}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> EditContactInfo(EditContactInfoViewModel model)
        {
            if (model.UserId != LoggedInUserId)
            {
                return RedirectToAction(MVC.Home.Restricted());
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _userManager.UpdateContactInfoAsync(model.UserId, model.FirstName, model.LastName, model.EmailAddress, model.PhoneNumber);
            FlashSuccess("Contact info updated.");
            return RedirectToAction(MVC.Profile.Details(model.UserId));
        }

        [GET("Profile/UploadResume/{UserId}")]
        public virtual ActionResult UploadResume(int userId)
        {
            if (userId != LoggedInUserId)
            {
                return RedirectToAction(MVC.Home.Restricted());
            }

            return View(new UploadResumeViewModel { UserId = userId });
        }

        [POST("Profile/UploadResume/{UserId}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> UploadResume(UploadResumeViewModel model)
        {
            if (model.UserId != LoggedInUserId)
            {
                return RedirectToAction(MVC.Home.Restricted());
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            byte[] fileBuffer = new byte[model.PostedFile.ContentLength];
            await model.PostedFile.InputStream.ReadAsync(fileBuffer, 0, model.PostedFile.ContentLength);
            await _resumeManager.UploadResumeAsync(model.UserId, model.PostedFile.FileName, model.PostedFile.ContentType, fileBuffer);
            FlashSuccess("Resume uploaded.");

            return RedirectToAction(MVC.Profile.Details(model.UserId));
        }

        [GET("Profile/DownloadResume/{UserId}")]
        public virtual async Task<ActionResult> DownloadResume(int userId)
        {
            var loggedInUser = await _userManager.LoadUserByIdAsync(LoggedInUserId.Value);

            // Consultants can only view their own resumes.  Account executives can view all resumes.
            if (loggedInUser is Consultant)
            {
                if (loggedInUser.Id != userId)
                {
                    return RedirectToAction(MVC.Home.Restricted());
                }
            }

            var resume = await _resumeManager.LoadResumeByUserIdAsync(userId);
            if (resume == null)
            {
                return HttpNotFound();
            }

            return File(resume.FileData, resume.Metadata.MimeType, resume.Metadata.FileName);
        }

        [GET("Profile/Skills/{ConsultantId}")]
        public virtual async Task<ActionResult> Skills(int consultantId)
        {
            var loggedInUser = await _userManager.LoadUserByIdAsync(LoggedInUserId.Value);
            if (loggedInUser.Id != consultantId)
            {
                return RedirectToAction(MVC.Home.Restricted());
            }
            var viewModel = await _skillsViewModelBuilder.BuildAsync(consultantId);
            return View(viewModel);
        }

        [POST("Profile/Skills/Add")]
        public virtual async Task<ActionResult> AddSkill(int consultantId, string skillName)
        {
            var loggedInUser = await _userManager.LoadUserByIdAsync(LoggedInUserId.Value);
            if (loggedInUser.Id != consultantId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            var skill = await _skillManager.AddSkillToConsultantAsync(skillName, consultantId);
            return Json(skill);
        }

        [POST("Profile/Skills/Remove")]
        public virtual async Task<ActionResult> RemoveSkill(int consultantId, int skillId)
        {
            var loggedInUser = await _userManager.LoadUserByIdAsync(LoggedInUserId.Value);
            if (loggedInUser.Id != consultantId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            await _skillManager.RemoveSkillFromConsultantAsync(consultantId, skillId);
            return Json(true);
        }
    }
}