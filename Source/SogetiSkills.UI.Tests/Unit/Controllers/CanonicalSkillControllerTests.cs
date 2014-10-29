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
using SogetiSkills.UI.ViewModels.CanonicalSkill;
using System.Web.Mvc;

namespace SogetiSkills.UI.Tests.Unit.Controllers
{
    public class CanonicalSkillControllerTests : ControllerUnitTestBase
    {
        protected Mock<ISkillManager> _fakeSkillManager = new Mock<ISkillManager>();

        public CanonicalSkillControllerTests()
        {
            _fixture.Inject(_fakeSkillManager);
        }

        [TestClass]
        public class List : CanonicalSkillControllerTests
        {
            [TestMethod]
            public async Task List_ReturnsViewWithAllCanonicalTags()
            {
                var tags = _fixture.CreateMany<Skill>();
                _fakeSkillManager.Setup(x => x.LoadCanonicalSkillsAsync()).Returns(Task.FromResult(tags));
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.List();

                AssertX.IsViewResultWithModel(actionResult, tags);
            }
        }

        [TestClass]
        public class Add : CanonicalSkillControllerTests
        {
            AddViewModel _validInput = new AddViewModel
                {
                    Name = "C#"
                };

            [TestMethod]
            public void Add_ReturnsViewResult()
            {
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = subject.Add();

                AssertX.IsViewResult(actionResult);
            }

            [TestMethod]
            public async Task Add_GivenInvalidInput_RedisplaysTheForm()
            {
                var subject = _fixture.Create<CanonicalSkillController>();
                subject.AddModelError();

                var viewModel = new AddViewModel();
                var actionResult = await subject.Add(viewModel);

                AssertX.IsViewResultWithModel(actionResult, viewModel);
            }

            [TestMethod]
            public async Task Add_GivenValidInput_AddsANewSkill()
            {
                var subject = _fixture.Create<CanonicalSkillController>();
                
                var actionResult = await subject.Add(_validInput);

                _fakeSkillManager.Verify(x => x.AddCanonicalSkillAsync("C#"));
            }

            [TestMethod]
            public async Task Add_GivenValidInput_RedirecstToTheListPageWithASuccessMessage()
            {
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.Add(_validInput);

                AssertX.FlashMessageSet(subject, "success", "C# was added.");
                AssertX.IsRedirectToRouteResult(actionResult, MVC.CanonicalSkill.Name, MVC.CanonicalSkill.ActionNames.List);
            }
        }

        [TestClass]
        public class Edit : CanonicalSkillControllerTests
        {
            EditViewModel _validInput = new EditViewModel
            {
                Id = 1,
                Name = "C#"
            };

            [TestMethod]
            public async Task Edit_GivenIdOfSkillThatDoesntExist_ReturnsHttpNotFound()
            {
                _fakeSkillManager.Setup(x => x.LoadByIdAsync(1)).Returns(Task.FromResult(null as Skill));
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.Edit(1);

                AssertX.Is404NotFoundResult(actionResult);
            }

            [TestMethod]
            public async Task Edit_GivenIdOfExistingSkill_ReturnsViewWithModel()
            {
                var cSharp = new Skill { Id = 1, Name = "C#", IsCanonical = true };
                _fakeSkillManager.Setup(x => x.LoadByIdAsync(1)).Returns(Task.FromResult(cSharp));
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.Edit(1);

                AssertX.IsViewResult(actionResult);
                var model = ((ViewResult)actionResult).Model as EditViewModel;
                Assert.AreEqual(1, model.Id);
                Assert.AreEqual("C#", model.Name);
            }

            [TestMethod]
            public async Task Edit_GivenInvalidInput_RedisplaysTheForm()
            {
                var subject = _fixture.Create<CanonicalSkillController>();
                subject.AddModelError();

                var viewModel = new EditViewModel();
                var actionResult = await subject.Edit(viewModel);

                AssertX.IsViewResultWithModel(actionResult, viewModel);
            }

            [TestMethod]
            public async Task Edit_GivenValidInput_UpdatesTheSkill()
            {
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.Edit(_validInput);

                _fakeSkillManager.Verify(x => x.UpdateSkillAsync(1, "C#", true));
            }

            [TestMethod]
            public async Task Add_GivenValidInput_RedirecstToTheListPageWithASuccessMessage()
            {
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.Edit(_validInput);

                AssertX.FlashMessageSet(subject, "success", "C# updated.");
                AssertX.IsRedirectToRouteResult(actionResult, MVC.CanonicalSkill.Name, MVC.CanonicalSkill.ActionNames.List);
            }
        }

        [TestClass]
        public class Delete : CanonicalSkillControllerTests
        {
            [TestMethod]
            public async Task Delete_GivenIdOfSkillThatDoesntExist_DoesTryToDeleteTheSkill()
            {
                _fakeSkillManager.Setup(x => x.LoadByIdAsync(456)).Returns(Task.FromResult(null as Skill));
                var subject = _fixture.Create<CanonicalSkillController>();

                await subject.Delete(456);

                _fakeSkillManager.Verify(x => x.RemoveCanonicalSkillAsync(456), Times.Never);
            }

            [TestMethod]
            public async Task Delete_GivenIdOfSkillThatDoesntExist_RedirectsToTheListPage()
            {
                _fakeSkillManager.Setup(x => x.LoadByIdAsync(456)).Returns(Task.FromResult(null as Skill));
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.Delete(456);

                AssertX.IsRedirectToRouteResult(actionResult, MVC.CanonicalSkill.Name, MVC.CanonicalSkill.ActionNames.List);
            }

            [TestMethod]
            public async Task Delete_GivenIdOfSkillThatExists_DeletesTheSkill()
            {
                var cSharp = new Skill { Id = 1, Name = "C#", IsCanonical = true };
                _fakeSkillManager.Setup(x => x.LoadByIdAsync(1)).Returns(Task.FromResult(cSharp));
                var subject = _fixture.Create<CanonicalSkillController>();

                await subject.Delete(1);

                _fakeSkillManager.Verify(x => x.RemoveCanonicalSkillAsync(1));
            }

            [TestMethod]
            public async Task Delete_GivenIdOfSkillThatExists_RedirecstToTheListPageWithASuccessMessage()
            {
                var cSharp = new Skill { Id = 1, Name = "C#", IsCanonical = true };
                _fakeSkillManager.Setup(x => x.LoadByIdAsync(1)).Returns(Task.FromResult(cSharp));
                var subject = _fixture.Create<CanonicalSkillController>();

                var actionResult = await subject.Delete(1);

                AssertX.FlashMessageSet(subject, "success", "C# was removed.");
                AssertX.IsRedirectToRouteResult(actionResult, MVC.CanonicalSkill.Name, MVC.CanonicalSkill.ActionNames.List);
            }
        }
    }
}
