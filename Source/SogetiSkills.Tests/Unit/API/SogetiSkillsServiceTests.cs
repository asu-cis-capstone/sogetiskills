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
using EntityFramework.Testing.Moq;

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
                List<SkillCategory> seed = new List<SkillCategory>();
                
                var fakeSkillCategories = new MockDbSet<SkillCategory>().SetupLinq().SetupSeedData(seed);
                fakeSkillCategories.Setup(x => x.Create()).Returns(new SkillCategory());
                var fakeDatabase = new Mock<SogetiSkillsDataContext>();
                fakeDatabase.Setup(x => x.SkillCategories).Returns(fakeSkillCategories.Object);

                SogetiSkillsService subject = new SogetiSkillsService(db: fakeDatabase.Object);
                
                subject.Skill_AddCateogry("Brand new category");

                fakeSkillCategories.Verify(x => x.Add(It.IsAny<SkillCategory>()));
                fakeDatabase.Verify(x => x.SaveChanges());
            }

            [TestMethod]
            public void Skill_AddCateogry_GivenACategoryThatExist_DoesNotAddANewCategory()
            {                
                var fakeSkillCategories = new MockDbSet<SkillCategory>()
                    .SetupLinq()
                    .SetupSeedData(new[] { new SkillCategory { Name = "AlreadyExists" } });
                var fakeDatabase = new Mock<SogetiSkillsDataContext>();
                fakeDatabase.Setup(x => x.SkillCategories).Returns(fakeSkillCategories.Object);

                SogetiSkillsService subject = new SogetiSkillsService(db: fakeDatabase.Object);

                subject.Skill_AddCateogry("AlreadyExists");

                fakeDatabase.Verify(x => x.SaveChanges(), Times.Never);
            }
        }
    }
}
