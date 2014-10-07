using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SogetiSkills.Core.Managers;
using SogetiSkills.UI.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using SogetiSkills.Core.Models;
using SogetiSkills.UI.Controllers;

namespace SogetiSkills.UI.Tests.Unit.Controllers
{
    public class CanonicalSkillControllerTests : ControllerUnitTestBase
    {
        protected Mock<ISkillManager> _fakeTagManager = new Mock<ISkillManager>();

        public CanonicalSkillControllerTests()
        {
            _fixture.Inject(_fakeTagManager);
        }

        [TestClass]
        public class List : CanonicalSkillControllerTests
        {
            [TestMethod]
            public async Task List_ReturnsViewWithAllCanonicalTags()
            {
                var tags = _fixture.CreateMany<Skill>();
                _fakeTagManager.Setup(x => x.LoadCanonicalSkillsAsync()).Returns(Task.FromResult(tags));
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.List();

                AssertX.IsViewResultWithModel(actionResult, tags);
            }
        }

        [TestClass]
        public class Add : CanonicalSkillControllerTests
        {
            [TestMethod]
            public void Add_ReturnsViewResult()
            {
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = subject.Add();

                AssertX.IsViewResult(actionResult);
            }
        }
    }
}
