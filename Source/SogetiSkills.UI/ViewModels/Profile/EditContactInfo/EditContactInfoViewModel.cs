using FluentValidation;
using FluentValidation.Attributes;
using SogetiSkills.Managers;
using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SogetiSkills.UI.ViewModels.Profile.EditContactInfo
{
    [Validator(typeof(EditContactInfoViewModelValidator))]
    public class EditContactInfoViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class EditContactInfoViewModelValidator : AbstractValidator<EditContactInfoViewModel>
    {
        private readonly IUserManager _userManager;

        public EditContactInfoViewModelValidator(IUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter a first name.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter a last name.");
            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Please enter an email address.")
                .EmailAddress().WithMessage("Email address is invalid")
                .Must(BeAvailable).WithMessage("There is already an account for {0}.", x => x.EmailAddress);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Please enter a phone number.")
                .Must(PhoneNumber.IsValid).WithMessage("Please enter a valid 10 digit phone number.");
        }

        private bool BeAvailable(EditContactInfoViewModel model, string emailAddress)
        {
            int? userId = _userManager.GetUserIdForEmailAddress(emailAddress);
            bool isAvailable = userId == null;
            bool alreadyBelongsToUser = userId == model.UserId;

            return isAvailable || alreadyBelongsToUser;
        }
    }
}