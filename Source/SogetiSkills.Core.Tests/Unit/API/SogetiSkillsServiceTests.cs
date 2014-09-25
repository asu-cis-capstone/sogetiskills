using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.API;
using SogetiSkills.Tests.TestHelpers;
using Ploeh;
using Ploeh.AutoFixture;
using Moq;
using SogetiSkills.API.Models;

namespace SogetiSkills.Tests.Unit.API
{
    public class SogetiSkillsServiceTests : UnitTestBase
    {
        [TestClass]
        public class Skill_AddCateogry : SogetiSkillsServiceTests
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Skill_AddCateogry_GivenAnEmptyName_ThrowsArgumentException()
            {
                SogetiSkillsService subject = new SogetiSkillsService(db: null);
                
                subject.Skill_AddCateogry(null);
            }

            [TestMethod]            
            public void Skill_AddCateogry_GivenACategoryThatDoesntExist_AddsTheCategory()
            {
                var fakeDatabase = new Mock<SogetiSkillsDataContext>();
                fakeDatabase.Setup(x => x.SkillCategories.)

                SogetiSkillsService subject = new SogetiSkillsService(db: fakeDatabase.Object);

                subject.Skill_AddCateogry(null);
            }
        }
    }
}
