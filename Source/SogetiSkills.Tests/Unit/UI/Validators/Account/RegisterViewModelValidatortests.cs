using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Tests.TestHelpers;
using SogetiSkills.UI.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using FluentValidation.TestHelper;
using Moq;
using SogetiSkills.Managers;

namespace SogetiSkills.Tests.Unit.UI.Validators.Account
{
    [TestClass]
    public class RegisterViewModelValidatorTests : UnitTestBase
    {
        [TestMethod]
        public void Validate_GivenEmptyEmailAddress_ShouldHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.EmailAddress, null as string);
        }

        [TestMethod]
        public void Validate_GivenInvalidEmailAddress_ShouldHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.EmailAddress, "invalid email address");
        }

        [TestMethod]
        public void Validate_GivenEmailAddressInUse_ShouldHaveError()
        {
            var fakeUserManager = new Mock<IUserManager>();
            fakeUserManager.Setup(x => x.IsEmailAddressInUse("already_in_use@site.com")).Returns(true);
            _fixture.Inject(fakeUserManager.Object);
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.EmailAddress, "already_in_use@site.com");
        }

        [TestMethod]
        public void Validate_GivenAvailableEmailAddress_ShouldNotHaveError()
        {
            var fakeUserManager = new Mock<IUserManager>();
            fakeUserManager.Setup(x => x.IsEmailAddressInUse("not_in_use@site.com")).Returns(false);
            _fixture.Inject(fakeUserManager.Object);
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, "not_in_use@site.com");
        }

        [TestMethod]
        public void Validate_GivenEmptyPassword_ShouldHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.Password, null as string);
        }

        [TestMethod]
        public void Validate_GivenPopulatedPassword_ShouldNotHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldNotHaveValidationErrorFor(x => x.Password, "pass");
        }

        [TestMethod]
        public void Validate_GivenPopulatedPasswordButDifferentConfirmPassword_ShouldHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();
            RegisterViewModel viewModel = new RegisterViewModel();
            viewModel.Password = "pass";
            viewModel.ConfirmPassword = "different pass";

            subject.ShouldHaveValidationErrorFor(x => x.ConfirmPassword, viewModel);
        }

        [TestMethod]
        public void Validate_GivenMatchingPasswords_ShouldNotHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();
            RegisterViewModel viewModel = new RegisterViewModel();
            viewModel.Password = "pass";
            viewModel.ConfirmPassword = "pass";

            subject.ShouldNotHaveValidationErrorFor(x => x.ConfirmPassword, viewModel);
        }

        [TestMethod]
        public void Validate_GivenEmptyFirstName_ShouldHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.FirstName, null as string);
        }

        [TestMethod]
        public void Validate_GivenPopulatedFirstName_ShouldNotHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldNotHaveValidationErrorFor(x => x.FirstName, "first name");
        }

        [TestMethod]
        public void Validate_GivenEmptyLastName_ShouldHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.LastName, null as string);
        }

        [TestMethod]
        public void Validate_GivenPopulatedLastName_ShouldNotHaveError()
        {
            RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

            subject.ShouldNotHaveValidationErrorFor(x => x.LastName, "last name");
        }
    }
}
