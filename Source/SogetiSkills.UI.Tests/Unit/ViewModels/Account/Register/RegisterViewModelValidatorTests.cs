using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using SogetiSkills.Core.Managers;
using SogetiSkills.UI.Tests.TestHelpers;
using SogetiSkills.UI.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.UI.Tests.Unit.UI.ViewModels.Account.Register
{
    public class RegisterViewModelValidatorTests : UnitTestBase
    {
        [TestClass]
        public class EmailAddress : RegisterViewModelValidatorTests
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
                fakeUserManager.Setup(x => x.GetUserIdForEmailAddress("already_in_use@site.com")).Returns(123);
                _fixture.Inject(fakeUserManager.Object);
                RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.EmailAddress, "already_in_use@site.com");
            }

            [TestMethod]
            public void Validate_GivenAvailableEmailAddress_ShouldNotHaveError()
            {
                var fakeUserManager = new Mock<IUserManager>();
                fakeUserManager.Setup(x => x.GetUserIdForEmailAddress("not_in_use@site.com")).Returns(null as int?);
                _fixture.Inject(fakeUserManager.Object);
                RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

                subject.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, "not_in_use@site.com");
            }
        }

        [TestClass]
        public class Password : RegisterViewModelValidatorTests
        {
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
        }

        [TestClass]
        public class AccountType : RegisterViewModelValidatorTests
        {
            [TestMethod]
            public void Validate_GivenEmptyAccountType_ShouldHaveError()
            {
                RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.AccountType, null as string);
            }

            [TestMethod]
            public void Validate_GivenInvalidAccounttype_ShouldHaveError()
            {
                RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.AccountType, "NotAValidAccounType");
            }

            [TestMethod]
            public void Validate_GivenValidAccounttype_ShouldNotHaveError()
            {
                RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

                subject.ShouldNotHaveValidationErrorFor(x => x.AccountType, "Consultant");
                subject.ShouldNotHaveValidationErrorFor(x => x.AccountType, "AccountExecutive");
            }
        }

        [TestClass]
        public class FirstName : RegisterViewModelValidatorTests
        {
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
        }

        [TestClass]
        public class LastName : RegisterViewModelValidatorTests
        {
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

        [TestClass]
        public class PhoneNumber : RegisterViewModelValidatorTests
        {
            [TestMethod]
            public void Validate_GivenEmptyPhoneNumber_ShouldHaveError()
            {
                RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.PhoneNumber, null as string);
            }

            [TestMethod]
            public void Validate_GivenInvalidPhoneNumber_ShouldHaveError()
            {
                RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.PhoneNumber, "123 Invalid 456 Phone Number");
            }

            [TestMethod]
            public void Validate_GivenValidPhoneNumber_ShouldNotHaveError()
            {
                RegisterViewModelValidator subject = _fixture.Create<RegisterViewModelValidator>();

                subject.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, "1234567890");
                subject.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, "(123) 456-7890");
                subject.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, "123-456-7890");
            }
        }
    }
}
