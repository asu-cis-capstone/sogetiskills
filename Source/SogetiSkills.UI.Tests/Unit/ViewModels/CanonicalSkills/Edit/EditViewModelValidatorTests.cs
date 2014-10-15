using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.UI.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using SogetiSkills.UI.ViewModels.Account;
using SogetiSkills.UI.ViewModels.CanonicalSkill;
using Ploeh.AutoFixture;
using Moq;
using SogetiSkills.Core.Managers;
using SogetiSkills.Core.Models;

namespace SogetiSkills.UI.Tests.Unit.UI.ViewModels.CanonicalSkills.Add
{
    [TestClass]
    public class EditViewModelValidatorTests : UnitTestBase
    {
        Mock<ISkillManager> _fakeSkillManager = new Mock<ISkillManager>();

        public EditViewModelValidatorTests()
        {
            _fixture.Inject(_fakeSkillManager);
        }

        [TestMethod]
        public void Validate_GiveEmptyName_ShouldHaveError()
        {
            var subject = _fixture.Create<EditViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.Name, null as string);
            subject.ShouldHaveValidationErrorFor(x => x.Name, string.Empty);
        }

        [TestMethod]
        public void Validate_GivenNameGreaterThant450Characters_ShouldHaveError()
        {
            var subject = _fixture.Create<EditViewModelValidator>();
            string moreThan450Characters = new string('a', 451);

            subject.ShouldHaveValidationErrorFor(x => x.Name, moreThan450Characters);
        }
    }
}