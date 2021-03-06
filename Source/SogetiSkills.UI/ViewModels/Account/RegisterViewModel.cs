﻿using FluentValidation;
using FluentValidation.Attributes;
using SogetiSkills.Core.Managers;
using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

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
                yield return new SelectListItem { Text = "Consultant", Value = "Consultant" };
                yield return new SelectListItem { Text = "Account Executive", Value = "AccountExecutive" };
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
                .NotEmpty().WithMessage("Please enter a password.")
                .Must(BeAtLeast7CharactersAndContainALetterAndANumber).WithMessage("Password must be at least 7 characters and contain a number and a letter.");
            
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
                .Must(PhoneNumber.IsValid).WithMessage("Please enter a valid 10 digit phone number.");
        }

        private bool BeAvailable(string emailAddress)
        {
            bool available = _userManager.GetUserIdForEmailAddress(emailAddress) == null;
            return available;
        }

        private bool BeInTheListOfValidAccountTypes(string accountType)
        {
            return AccountTypes.All.Contains(accountType);
        }

        private bool BeAtLeast7CharactersAndContainALetterAndANumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return true;
            }

            return input.Length >= 7
                && input.Any(x => char.IsDigit(x))
                && input.Any(x => char.IsLetter(x));
        }
    }
}