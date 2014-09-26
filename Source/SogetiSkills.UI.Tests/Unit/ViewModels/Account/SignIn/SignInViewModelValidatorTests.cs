using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.UI.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using SogetiSkills.UI.ViewModels.Account;

namespace SogetiSkills.UI.Tests.Unit.UI.ViewModels.Account.SignIn
{
    [TestClass]
    public class SignInViewModelValidatorTests : UnitTestBase
    {
        [TestMethod]
        public void Validate_GivenEmptyEmailAddress_ShouldHaveError()
        {
            SignInViewModelValidator subject = new SignInViewModelValidator();

            subject.ShouldHaveValidationErrorFor(x => x.EmailAddress, null as string);
        }

        [TestMethod]
        public void Validate_GivenPopulatedEmailAddress_ShouldNotHaveError()
        {
            SignInViewModelValidator subject = new SignInViewModelValidator();

            subject.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, "user@site.com");
        }

        [TestMethod]
        public void Validate_GivenEmptyPassword_ShouldHaveError()
        {
            SignInViewModelValidator subject = new SignInViewModelValidator();

            subject.ShouldHaveValidationErrorFor(x => x.Password, null as string);
        }

        [TestMethod]
        public void Validate_GivenPopulatedPassword_ShouldNotHaveError()
        {
            SignInViewModelValidator subject = new SignInViewModelValidator();

            subject.ShouldNotHaveValidationErrorFor(x => x.Password, "pass");
        }
    }
}