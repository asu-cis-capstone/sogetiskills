using Moq;
using SogetiSkills.Core.Managers;
using SogetiSkills.UI.Tests.TestHelpers;
using SogetiSkills.UI.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Core.Models;
using SogetiSkills.UI.ViewModels.Profile.Details;

namespace SogetiSkills.UI.Tests.Unit.UI.ViewModels.Profile.Details
{
    [TestClass]
    public class DetailsViewModelBuilderTests : UnitTestBase
    {
        protected Mock<IUserManager> _fakeUserManager = new Mock<IUserManager>();
        protected Mock<IResumeManager> _fakeResumeManager = new Mock<IResumeManager>();
        protected Mock<ISkillManager> _fakeTagManager = new Mock<ISkillManager>();

        public DetailsViewModelBuilderTests ()
	    {
            _fixture.Inject(_fakeUserManager);
            _fixture.Inject(_fakeResumeManager);
            _fixture.Inject(_fakeTagManager);
	    }

        [TestMethod]
        public async Task BuildAsync_GivenTheIdOfAUserThatDoesntExist_ReturnsNull()
        {
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult(null as User));
            DetailsViewModelBuilder subject = _fixture.Create<DetailsViewModelBuilder>();

            var viewModel = await subject.BuildAsync(profileUserId: 123, loggedInUserId: 0);

            Assert.IsNull(viewModel);
        }

        [TestMethod]
        public async Task BuildAsync_GivenUser_SetsCommonUserInfo()
        {
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult((User)SampleData.Consultant()));
            DetailsViewModelBuilder subject = _fixture.Create<DetailsViewModelBuilder>();

            var viewModel = await subject.BuildAsync(profileUserId: 123, loggedInUserId: 0);

            Assert.AreEqual(viewModel.UserId, 123);
            Assert.AreEqual(viewModel.Email, "bill@site.com");
            Assert.AreEqual(viewModel.FirstName, "Bill");
            Assert.AreEqual(viewModel.LastName, "Smith");
            Assert.AreEqual(viewModel.FullName, "Bill Smith");
            Assert.AreEqual(viewModel.PhoneNumber, "(123) 456-7890");
            Assert.AreEqual(viewModel.IsOnBeach, true);
        }

        [TestMethod]
        public async Task BuildAsync_GivenAccountExecutive_SetsUserTypeDescriptionToAccountExecutive()
        {
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(456)).Returns(Task.FromResult((User)SampleData.AccountExecutive()));
            DetailsViewModelBuilder subject = _fixture.Create<DetailsViewModelBuilder>();

            var viewModel = await subject.BuildAsync(profileUserId: 456, loggedInUserId: 0);

            Assert.AreEqual(viewModel.UserTypeDescription, "Account Executive");
        }

        [TestMethod]
        public async Task BuildAsync_GivenConsultantWithResume_LoadsResume()
        {
            var consultant = SampleData.Consultant();
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult((User)consultant));
            var resume = SampleData.ResumeMetadata();
            _fakeResumeManager.Setup(x => x.LoadResumeMetadataByUserIdAsync(123)).Returns(Task.FromResult(resume));
            DetailsViewModelBuilder subject = _fixture.Create<DetailsViewModelBuilder>();

            var viewModel = await subject.BuildAsync(profileUserId: 123, loggedInUserId: 0);

            Assert.AreEqual(viewModel.ResumeMetadata, resume);
        }

        [TestMethod]
        public async Task BuildAsync_GivenConsultantWithoutResume_LeavesResumeNull()
        {
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult((User)SampleData.Consultant()));
            _fakeResumeManager.Setup(x => x.LoadResumeMetadataByUserIdAsync(123)).Returns(Task.FromResult(null as ResumeMetadata));
            DetailsViewModelBuilder subject = _fixture.Create<DetailsViewModelBuilder>();

            var viewModel = await subject.BuildAsync(profileUserId: 123, loggedInUserId: 0);

            Assert.IsNull(viewModel.ResumeMetadata);            
        }

        [TestMethod]
        public async Task BuildAsync_GivenConsultant_LoadsTagsOrderedByName()
        {
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult((User)SampleData.Consultant()));
            var consultantSkills = SampleData.ConsultantSkillList(123);
            var consultantSkillsOrderedByName = consultantSkills.OrderBy(x => x.SkillName);
            _fakeTagManager.Setup(x => x.LoadSkillsForConsultantAsync(123)).Returns(Task.FromResult(consultantSkills));
            DetailsViewModelBuilder subject = _fixture.Create<DetailsViewModelBuilder>();

            var viewModel = await subject.BuildAsync(profileUserId: 123, loggedInUserId: 0);

            Assert.IsTrue(consultantSkillsOrderedByName.SequenceEqual(viewModel.ConsultantSkills));
        }

        [TestMethod]
        public async Task BuildAsync_GivenUserViewingOwnProfile_SetsProfileBelongsToCurrentUserTrue()
        {
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult((User)SampleData.Consultant()));
            DetailsViewModelBuilder subject = _fixture.Create<DetailsViewModelBuilder>();

            var viewModel = await subject.BuildAsync(profileUserId: 123, loggedInUserId: 123);

            Assert.IsTrue(viewModel.ProfileBelongsToCurrentUser);
        }

        [TestMethod]
        public async Task BuildAsync_GivenUserViewingOthersProfile_SetsProfileBelongsToCurrentUserFalse()
        {
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult((User)SampleData.Consultant()));
            DetailsViewModelBuilder subject = _fixture.Create<DetailsViewModelBuilder>();

            var viewModel = await subject.BuildAsync(profileUserId: 123, loggedInUserId: 456);

            Assert.IsFalse(viewModel.ProfileBelongsToCurrentUser);
        }
    }
}
