using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SogetiSkills.Tests.TestHelpers;
using SogetiSkills.UI.Controllers;
using SogetiSkills.UI.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using System.Web.Mvc;
using SogetiSkills.UI.ViewModels.Profile.EditContactInfo;
using SogetiSkills.UI.ViewModels.Profile.Details;
using SogetiSkills.Managers;

namespace SogetiSkills.Tests.Unit.UI.Controllers
{
    public class ProfileControllerTests : ControllerUnitTestBase
    {
        public ProfileControllerTests()
        {
            SetLoggedInUserId(123);
        }

        [TestClass]
        public class Details : ProfileControllerTests
        {
            [TestMethod]
            public async Task Details_GivenIdForUserThatDoesntExist_Returns404()
            {
                var fakeDetailsViewModelBuilder = new Mock<IDetailsViewModelBuilder>();
                fakeDetailsViewModelBuilder.Setup(x => x.BuildAsync(It.IsAny<int>(), It.IsAny<int>())).Returns(Task.FromResult(null as DetailsViewModel));
                _fixture.Inject(fakeDetailsViewModelBuilder);
                ProfileController subject = _fixture.Create<ProfileController>();

                ActionResult actionResult = await subject.Details(123);

                AssertX.Is404NotFoundResult(actionResult);
            }

            [TestMethod]
            public async Task Details_GivenIdForUser_ReturnsDetailsViewModel()
            {
                var fakeDetailsViewModelBuilder = new Mock<IDetailsViewModelBuilder>();
                fakeDetailsViewModelBuilder.Setup(x => x.BuildAsync(123, 123)).Returns(Task.FromResult(new DetailsViewModel()));
                _fixture.Inject(fakeDetailsViewModelBuilder);
                ProfileController subject = _fixture.Create<ProfileController>();

                ActionResult actionResult = await subject.Details(123);

                AssertX.IsViewResultWithModelOfType<DetailsViewModel>(actionResult);
            }
        }

        [TestClass]
        public class EditContactInfo : ProfileControllerTests
        {
            Mock<IEditContactInfoViewModelBuilder> _fakeEditContactInfoViewModelBuilder;
            Mock<IUserManager> _fakeUserManager;
            EditContactInfoViewModel _validInput;

            public EditContactInfo()
            {
                _fakeEditContactInfoViewModelBuilder = new Mock<IEditContactInfoViewModelBuilder>();
                _fixture.Inject(_fakeEditContactInfoViewModelBuilder);

                _fakeUserManager = new Mock<IUserManager>();
                _fixture.Inject(_fakeUserManager);

                _validInput = new EditContactInfoViewModel
                {
                    UserId = 123,
                    FirstName = "Bill",
                    LastName = "Smith",
                    EmailAddress = "bill@site.com",
                    PhoneNumber = "1234567890"
                };
            }

            [TestMethod]
            public async Task EditContactInfo_GivenIdNotForCurrentUser_ReturnsUnauthorized()
            {
                SetLoggedInUserId(456);
                ProfileController subject = _fixture.Create<ProfileController>();

                ActionResult actionResult = await subject.EditContactInfo(123);

                AssertX.IsRedirectToRestrictedPage(actionResult);
            }

            [TestMethod]
            public async Task EditContactInfo_GivenIdForUserThatDoesntExist_Returns404()
            {
                _fakeEditContactInfoViewModelBuilder.Setup(x => x.BuildAsync(It.IsAny<int>())).Returns(Task.FromResult(null as EditContactInfoViewModel));
                ProfileController subject = _fixture.Create<ProfileController>();

                ActionResult actionResult = await subject.EditContactInfo(123);

                AssertX.Is404NotFoundResult(actionResult);
            }

            [TestMethod]
            public async Task EditContactInfo_GivenIdForUser_ReturnsEditContactInfoViewModel()
            {
                _fakeEditContactInfoViewModelBuilder.Setup(x => x.BuildAsync(123)).Returns(Task.FromResult(new EditContactInfoViewModel()));
                ProfileController subject = _fixture.Create<ProfileController>();

                ActionResult actionResult = await subject.EditContactInfo(123);

                AssertX.IsViewResultWithModelOfType<EditContactInfoViewModel>(actionResult);
            }

            [TestMethod]
            public async Task EditContactInfo_GivenIdForDifferentUser_ReturnsRestrictedPage()
            {
                SetLoggedInUserId(123);
                EditContactInfoViewModel model = new EditContactInfoViewModel { UserId = 456 };
                ProfileController subject = _fixture.Create<ProfileController>();

                ActionResult actionResult = await subject.EditContactInfo(model);

                AssertX.IsRedirectToRestrictedPage(actionResult);
            }

            [TestMethod]
            public async Task EditContactInfo_GivenInvalidInput_RedisplaysTheForm()
            {
                SetLoggedInUserId(123);
                EditContactInfoViewModel model = new EditContactInfoViewModel { UserId = 123 };
                ProfileController subject = _fixture.Create<ProfileController>();
                subject.AddModelError();

                ActionResult actionResult = await subject.EditContactInfo(model);

                AssertX.IsViewResultWithModel(actionResult, model);
            }

            [TestMethod]
            public async Task EditContactInfo_GivenValidInput_UpdatesContactInfo()
            {
                SetLoggedInUserId(123);
                ProfileController subject = _fixture.Create<ProfileController>();

                await subject.EditContactInfo(_validInput);

                _fakeUserManager.Verify(x => x.UpdateContactInfoAsync(123, "Bill", "Smith", "bill@site.com", "1234567890"));
            }

            [TestMethod]
            public async Task EditContactInfo_GivenValidInput_SetsAFlashMessage()
            {
                SetLoggedInUserId(123);
                ProfileController subject = _fixture.Create<ProfileController>();

                await subject.EditContactInfo(_validInput);

                AssertX.FlashMessageSet(subject, "success", "Contact info updated.");
            }

            [TestMethod]
            public async Task EditContactInfo_GivenValidInput_RedirectsToProfileDetails()
            {
                SetLoggedInUserId(123);
                ProfileController subject = _fixture.Create<ProfileController>();

                ActionResult actionResult = await subject.EditContactInfo(_validInput);

                AssertX.IsRedirectToRouteResult(actionResult, MVC.Profile.Name, MVC.Profile.ActionNames.Details, new { userId = 123 });
            }
        }
    }
}
