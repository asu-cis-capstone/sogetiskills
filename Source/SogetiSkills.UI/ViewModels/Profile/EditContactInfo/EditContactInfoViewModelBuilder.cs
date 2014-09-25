using SogetiSkills.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SogetiSkills.UI.ViewModels.Profile.EditContactInfo
{
    public class EditContactInfoViewModelBuilder : IEditContactInfoViewModelBuilder
    {
        private readonly IUserManager _userManager;

        public EditContactInfoViewModelBuilder(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<EditContactInfoViewModel> BuildAsync(int userId)
        {
            var user = await _userManager.LoadUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            EditContactInfoViewModel viewModel = new EditContactInfoViewModel();

            viewModel.UserId = user.Id;
            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.EmailAddress = user.EmailAddress;
            viewModel.PhoneNumber = user.PhoneNumber.Value;

            return viewModel;
        }
    }
}