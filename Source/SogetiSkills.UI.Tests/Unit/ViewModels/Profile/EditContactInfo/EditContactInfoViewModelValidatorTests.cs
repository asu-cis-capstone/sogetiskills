using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.UI.Tests.TestHelpers;
using SogetiSkills.UI.ViewModels.Profile.EditContactInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Ploeh.AutoFixture;
using SogetiSkills.Core.Managers;
using Moq;

namespace SogetiSkills.UI.Tests.Unit.UI.ViewModels.Profile.EditContactInfo
{
    public class EditContactInfoViewModelValidatorTests : UnitTestBase
    {
        [TestClass]
        public class EmailAddress : EditContactInfoViewModelValidatorTests
        {
            private readonly Mock<IUserManager> _fakeUserManager;

            public EmailAddress()
            {
                _fakeUserManager = new Mock<IUserManager>();
                _fixture.Inject(_fakeUserManager);
            }

            [TestMethod]
            public void Validate_GivenEmptyEmailAddress_ShouldHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.EmailAddress, null as string);
            }

            [TestMethod]
            public void Validate_GivenInvalidEmailAddress_ShouldHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.EmailAddress, "invalid email address");
            }

            [TestMethod]
            public void Validate_GivenEmailAddressInUseByDifferentUser_ShouldHaveError()
            {
                _fakeUserManager.Setup(x => x.GetUserIdForEmailAddress("different_user@site.com")).Returns(456);
                EditContactInfoViewModel model = new EditContactInfoViewModel 
                { 
                    UserId = 123,
                    EmailAddress = "different_user@site.com"
                };
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.EmailAddress, model);
            }

            [TestMethod]
            public void Validate_GivenCurrentEmailAddress_ShouldNotHaveError()
            {
                _fakeUserManager.Setup(x => x.GetUserIdForEmailAddress("bill@site.com")).Returns(123);
                EditContactInfoViewModel model = new EditContactInfoViewModel
                {
                    UserId = 123,
                    EmailAddress = "bill@site.com"
                };
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, model);
            }

            [TestMethod]
            public void Validate_GivenAvailableEmailAddress_ShouldNotHaveError()
            {
                _fakeUserManager.Setup(x => x.GetUserIdForEmailAddress("not_in_use@site.com")).Returns(null as int?);
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldNotHaveValidationErrorFor(x => x.EmailAddress, "not_in_use@site.com");
            }
        }

        [TestClass]
        public class FirstName : EditContactInfoViewModelValidatorTests
        {
            [TestMethod]
            public void Validate_GivenEmptyFirstName_ShouldHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.FirstName, null as string);
            }

            [TestMethod]
            public void Validate_GivenPopulatedFirstName_ShouldNotHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldNotHaveValidationErrorFor(x => x.FirstName, "first name");
            }
        }

        [TestClass]
        public class LastName : EditContactInfoViewModelValidatorTests
        {
            [TestMethod]
            public void Validate_GivenEmptyLastName_ShouldHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.LastName, null as string);
            }

            [TestMethod]
            public void Validate_GivenPopulatedLastName_ShouldNotHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldNotHaveValidationErrorFor(x => x.LastName, "last name");
            }
        }

        [TestClass]
        public class PhoneNumber : EditContactInfoViewModelValidatorTests
        {
            [TestMethod]
            public void Validate_GivenEmptyPhoneNumber_ShouldHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.PhoneNumber, null as string);
            }

            [TestMethod]
            public void Validate_GivenInvalidPhoneNumber_ShouldHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldHaveValidationErrorFor(x => x.PhoneNumber, "123 Invalid 456 Phone Number");
            }

            [TestMethod]
            public void Validate_GivenValidPhoneNumber_ShouldNotHaveError()
            {
                EditContactInfoViewModelValidator subject = _fixture.Create<EditContactInfoViewModelValidator>();

                subject.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, "1234567890");
                subject.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, "(123) 456-7890");
                subject.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber, "123-456-7890");
            }
        }
    }
}
