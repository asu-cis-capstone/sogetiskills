using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SogetiSkills.Managers;
using SogetiSkills.Models;
using SogetiSkills.Tests.TestHelpers;
using SogetiSkills.UI.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using SogetiSkills.UI.ViewModels.Profile.EditContactInfo;

namespace SogetiSkills.Tests.Unit.UI.ViewModels.Profile
{
    [TestClass]
    public class EditContactInfoViewModelBuilderTests : UnitTestBase
    {
        protected Mock<IUserManager> _fakeUserManager = new Mock<IUserManager>();

        public EditContactInfoViewModelBuilderTests()
	    {
            _fixture.Inject(_fakeUserManager);
	    }

        [TestMethod]
        public async Task BuildAsync_GivenTheIdOfAUserThatDoesntExist_ReturnsNull()
        {           
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult(null as User));
            EditContactInfoViewModelBuilder subject = _fixture.Create<EditContactInfoViewModelBuilder>();

            var viewModel = await subject.BuildAsync(123);

            Assert.IsNull(viewModel);
        }

        [TestMethod]
        public async Task BuildAsync_GivenUser_SetsContactInfo()
        {
            _fakeUserManager.Setup(x => x.LoadUserByIdAsync(123)).Returns(Task.FromResult((User)SampleData.Consultant()));
            EditContactInfoViewModelBuilder subject = _fixture.Create<EditContactInfoViewModelBuilder>();

            var viewModel = await subject.BuildAsync(123);

            Assert.AreEqual(123, viewModel.UserId);
            Assert.AreEqual("Bill", viewModel.FirstName);
            Assert.AreEqual("Smith", viewModel.LastName);
            Assert.AreEqual("bill@site.com", viewModel.EmailAddress);
            Assert.AreEqual("1234567890", viewModel.PhoneNumber);
        }
    }
}
