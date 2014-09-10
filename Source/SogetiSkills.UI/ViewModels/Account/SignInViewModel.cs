using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SogetiSkills.UI.ViewModels.Account
{
    [Validator(typeof(SignInViewModelValidator))]
    public class SignInViewModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }

    public class SignInViewModelValidator : AbstractValidator<SignInViewModel>
    {
        public SignInViewModelValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("Email address is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        }
    }
}