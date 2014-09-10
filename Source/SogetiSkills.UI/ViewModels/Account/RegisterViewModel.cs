using FluentValidation;
using FluentValidation.Attributes;
using SogetiSkills.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SogetiSkills.UI.ViewModels.Account
{
    [Validator(typeof(RegisterViewModelValidator))]
    public class RegisterViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        private readonly IUserManager _userManager;

        public RegisterViewModelValidator(IUserManager userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Please enter an email address.")
                .EmailAddress().WithMessage("Email address is invalid")
                .Must(BeAvailable).WithMessage("There is already an account for {0}.", x => x.EmailAddress);
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please enter a password.");
            
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).When(x => !string.IsNullOrEmpty(x.Password)).WithMessage("Passwords do not match.");
            
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Please enter a first name.");
            
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Please enter a last name.");
        }

        public bool BeAvailable(string emailAddress)
        {
            bool inUse = _userManager.IsEmailAddressInUse(emailAddress);
            return !inUse;
        }
    }
}