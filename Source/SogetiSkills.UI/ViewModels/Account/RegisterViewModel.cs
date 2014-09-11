using FluentValidation;
using FluentValidation.Attributes;
using SogetiSkills.Managers;
using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Humanizer;

namespace SogetiSkills.UI.ViewModels.Account
{
    [Validator(typeof(RegisterViewModelValidator))]
    public class RegisterViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string AccountType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<SelectListItem> AccountTypeOptions
        {
            get
            {
                return from x in AccountTypes.All
                       select new SelectListItem
                       {
                           Text = x.Humanize(LetterCasing.Title),
                           Value = x
                       };
            }
        }
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

            RuleFor(x => x.AccountType)
                .NotEmpty().WithMessage("Please select an account type.")
                .Must(BeInTheListOfValidAccountTypes).WithMessage("Please select an account type.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Please enter a first name.");
            
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Please enter a last name.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Please enter a phone number.")
                .Must(BeAValid10DigitPhoneNumber).WithMessage("Please enter a valid 10 digit phone number.");
        }

        private bool BeAvailable(string emailAddress)
        {
            bool inUse = _userManager.IsEmailAddressInUse(emailAddress);
            return !inUse;
        }

        private bool BeInTheListOfValidAccountTypes(string accountType)
        {
            return AccountTypes.All.Contains(accountType);
        }

        private bool BeAValid10DigitPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return true;
            }

            string phoneNumberStrippedOfPunctuation = phoneNumber
                .Replace(" ", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace("-", string.Empty)
                .Trim();

            return phoneNumberStrippedOfPunctuation.Length == 10 
                && phoneNumberStrippedOfPunctuation.All(x => char.IsDigit(x));
        }
    }
}