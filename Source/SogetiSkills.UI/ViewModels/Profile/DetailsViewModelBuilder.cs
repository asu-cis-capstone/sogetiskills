using SogetiSkills.Managers;
using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SogetiSkills.UI.ViewModels.Profile
{
    public class DetailsViewModelBuilder : IDetailsViewModelBuilder
    {
        private readonly IUserManager _userManager;
        private readonly IResumeManager _resumeManager;
        private readonly ITagManager _tagManager;

        public DetailsViewModelBuilder(
            IUserManager userManager, 
            IResumeManager resumeManager, 
            ITagManager tagManager)
        {
            _userManager = userManager;
            _resumeManager = resumeManager;
            _tagManager = tagManager;
        }

        public async Task<DetailsViewModel> BuildAsync(int profileUserId, int loggedInUserId)
        {
            DetailsViewModel model = new DetailsViewModel();

            User user = await _userManager.LoadUserByIdAsync(profileUserId);
            if (user == null)
            {
                return null;
            }

            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.FullName = string.Format("{0} {1}", user.FirstName, user.LastName);
            model.Email = user.EmailAddress;
            model.PhoneNumber = user.PhoneNumber.GetFormattedValue();

            if (user is Consultant)
            {
                Consultant consultant = (Consultant)user;
                model.UserTypeDescription = "Consultant";
                model.IsConsultant = true;
                model.IsOnBeach = consultant.IsOnBeach ?? false;
                model.ResumeMetadata = await _resumeManager.LoadResumeMetadata(consultant.Id);
                model.Tags = await _tagManager.LoadTagsForConsultant(consultant.Id);
            }
            else if (user is AccountExecutive)
            {
                model.UserTypeDescription = "Account Executive";
            }

            model.ProfileBelongsToCurrentUser = profileUserId == loggedInUserId;

            return model;
        }
    }
}