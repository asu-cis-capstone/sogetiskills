using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SogetiSkills.UI.Tests.TestHelpers;
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
using SogetiSkills.Core.Managers;
using SogetiSkills.Core.Models;

namespace SogetiSkills.UI.Tests.Unit.UI.Controllers
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

        [TestClass]
        public class DownloadResume : ProfileControllerTests
        {
            protected readonly Mock<IUserManager> _fakeUserManager = new Mock<IUserManager>();
            protected readonly Mock<IResumeManager> _fakeResumeManager = new Mock<IResumeManager>();

            public DownloadResume()
            {
                _fixture.Inject(_fakeUserManager);
                _fixture.Inject(_fakeResumeManager);
            }

            [TestMethod]
            public async Task DownloadResume_GivenConsultantIsDownloadingOtherConsultantsResume_ReturnsRestricted()
            {
                // consultant with id 123 is trying to download resume for consultant #2
                var consultant123 = (User)SampleData.Consultant();
                _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult(consultant123));
                SetLoggedInUserId(123);
                var subject = _fixture.Create<ProfileController>();

                var actionResult = await subject.DownloadResume(2);

                AssertX.IsRedirectToRestrictedPage(actionResult);
            }

            [TestMethod]
            public async Task DownloadResume_GivenConsultantIsDownloadingOwnResume_ReturnsResume()
            {
                // consultant with id 123 is trying to download their own resume
                var consultant123 = (User)SampleData.Consultant();
                _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult(consultant123));
                var resume = SampleData.Resume(userId: 123);
                _fakeResumeManager.Setup(x => x.LoadResumeByUserIdAsync(123)).Returns(Task.FromResult(resume));
                SetLoggedInUserId(123);
                var subject = _fixture.Create<ProfileController>();

                var actionResult = await subject.DownloadResume(123);

                Assert.IsInstanceOfType(actionResult, typeof(FileContentResult));
            }

            [TestMethod]
            public async Task DownloadResume_GivenAccountExecutiveISDownloadingResume_ReturnsResume()
            {
                // account exec with id 456 is trying to download the resume for consultant #123
                var accountExecutive456 = (User)SampleData.AccountExecutive();
                _fakeUserManager.Setup(x => x.LoadUserByIdAsync(456)).Returns(Task.FromResult(accountExecutive456));
                var resume = SampleData.Resume(userId: 123);
                _fakeResumeManager.Setup(x => x.LoadResumeByUserIdAsync(123)).Returns(Task.FromResult(resume));
                SetLoggedInUserId(456);
                var subject = _fixture.Create<ProfileController>();

                var actionResult = await subject.DownloadResume(123);

                Assert.IsInstanceOfType(actionResult, typeof(FileContentResult));
            }

            [TestMethod]
            public async Task DownloadResume_GivenUserThatHasNoResume_ReturnsHttpNotFound()
            {
                // account exec with id 456 is trying to download a resume for a consultant that doesn't have one
                var accountExecutive456 = (User)SampleData.AccountExecutive();
                _fakeUserManager.Setup(x => x.LoadUserByIdAsync(456)).Returns(Task.FromResult(accountExecutive456));
                _fakeResumeManager.Setup(x => x.LoadResumeByUserIdAsync(123)).Returns(Task.FromResult((Resume)null));
                SetLoggedInUserId(456);
                var subject = _fixture.Create<ProfileController>();

                var actionResult = await subject.DownloadResume(123);

                AssertX.Is404NotFoundResult(actionResult);
            }
        }
    }
}
