using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Core.Managers;
using SogetiSkills.Core.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;

namespace SogetiSkills.Core.Tests.Unit.Managers
{
    public class SkillManagerTests : DbUnitTestBase
    {
        [TestClass]
        public class LoadSkillsForConsulant : SkillManagerTests
        {
            [TestMethod]
            public async Task LoadSkillsForConsultant_GivenConsultantWithNoSkills_ReturnsEmptySet()
            {
                int userId = InsertUser(SampleData.Consultant());
                var subject = _fixture.Create<SkillManager>();

                var skills = await subject.LoadSkillsForConsultantAsync(userId);

                Assert.AreEqual(0, skills.Count());
            }

            [TestMethod]
            public async Task LoadSkillsForConsultant_GivenConsultantWithSkills_ReturnsSkills()
            {
                int userId = InsertUser(SampleData.Consultant());
                var cSharp = InsertSkill("C#", "C# description", true);
                var aspNet = InsertSkill("ASP.NET", "ASP.NET description", false);
                InsertConsultantSkill(userId, cSharp.Id);
                InsertConsultantSkill(userId, aspNet.Id);

                var subject = _fixture.Create<SkillManager>();

                var skills = await subject.LoadSkillsForConsultantAsync(userId);

                Assert.AreEqual(2, skills.Count());
            }
        }

        [TestClass]
        public class LoadCanonicalSkills : SkillManagerTests
        {
            [TestMethod]
            public async Task LoadCanonicalSkills_ReturnsOnlyCanonicalSkills()
            {
                InsertSkill("C#", "C# description", true);
                InsertSkill("ASP.NET", "ASP.NET description", false);
                InsertSkill("JavaScript", "JavaScript description", true);

                var subject = _fixture.Create<SkillManager>();

                var skills = await subject.LoadCanonicalSkillsAsync();

                Assert.AreEqual(2, skills.Count());
                Assert.IsTrue(skills.All(x => x.IsCanonical));
            }


            [TestMethod]
            public async Task LoadCanonicalSkills_ReturnsSkillsOrderedByKeyword()
            {
                InsertSkill("C#", "C# description", true);
                InsertSkill("ASP.NET", "ASP.NET description", true);
                InsertSkill("JavaScript", "JavaScript description", true);

                var subject = _fixture.Create<SkillManager>();

                var skills = await subject.LoadCanonicalSkillsAsync();

                Assert.AreEqual("ASP.NET", skills.ElementAt(0).Name);
                Assert.AreEqual("C#", skills.ElementAt(1).Name);
                Assert.AreEqual("JavaScript", skills.ElementAt(2).Name);
            }
        }

        [TestClass]
        public class AddCanonicalSkill : SkillManagerTests
        {
            [TestMethod]
            public async Task AddCanonicalSkill_GivenNewSkill_Inserts()
            {
                var subject = _fixture.Create<SkillManager>();

                await subject.AddCanonicalSkillAsync("C#", "C# description");

                int count = (int)TestDatabase.QueryValue("SELECT COUNT(*) FROM Skills WHERE Name = @0", "C#");
                Assert.AreEqual(1, count);
            }

            [TestMethod]
            public async Task AddCanonicalSkill_GivenNameThatAlreadyExists_UpdatesExistingSkill()
            {
                var existingSkill = InsertSkill("C#", "C# description", isCanonical: false);
                var subject = _fixture.Create<SkillManager>();

                await subject.AddCanonicalSkillAsync("C#", "new C# description");

                dynamic newCanonicalSkill = TestDatabase.QuerySingle("SELECT Id, Name, [Description], IsCanonical FROM Skills WHERE Id = @0", existingSkill.Id);
                Assert.AreEqual(existingSkill.Id, newCanonicalSkill.Id);
                Assert.AreEqual("C#", newCanonicalSkill.Name);
                Assert.AreEqual("new C# description", newCanonicalSkill.Description);
                Assert.AreEqual(true, newCanonicalSkill.IsCanonical);
            }
        }

        [TestClass]
        public class RemoveCanonicalSkill : SkillManagerTests
        {
            [TestMethod]
            public async Task RemoveCanonicalSkill_GivenCanonicalSkillInUse_SetsIsCanonicalToFalse()
            {
                int userId = InsertUser(SampleData.Consultant());
                var canonicalSkill = InsertSkill("C#", "C# description", true);
                InsertConsultantSkill(userId, canonicalSkill.Id);

                var subject = _fixture.Create<SkillManager>();

                await subject.RemoveCanonicalSkillAsync(canonicalSkill.Id);

                bool isCanonical = TestDatabase.QueryValue("SELECT IsCanonical FROM Skills WHERE Id = @0", canonicalSkill.Id);
                Assert.IsFalse(isCanonical);
            }

            [TestMethod]
            public async Task RemoveCanonicalSkill_GivenCanonicalSkillNotInUse_DeletesSkill()
            {
                var canonicalSkill = InsertSkill("C#", "C# description", true);

                var subject = _fixture.Create<SkillManager>();

                await subject.RemoveCanonicalSkillAsync(canonicalSkill.Id);

                int count = TestDatabase.QueryValue("SELECT COUNT(*) FROM Skills WHERE Id = @0", canonicalSkill.Id);
                Assert.AreEqual(0, count);
            }
        }

        [TestClass]
        public class UpdateSkill : SkillManagerTests
        {
            [TestMethod]
            public async Task UpdateSkill_GivenAllNewValues_UpdatesTheSkill()
            {
                var skill = InsertSkill("C#", "C# description", true);

                var subject = _fixture.Create<SkillManager>();

                await subject.UpdateSkillAsync(skill.Id, "New Name", "New Description", false);

                var updatedSkill = TestDatabase.QuerySingle("SELECT Name, [Description], IsCanonical FROM Skills WHERE Id = @0", skill.Id);
                Assert.AreEqual("New Name", updatedSkill.Name);
                Assert.AreEqual("New Description", updatedSkill.Description);
                Assert.AreEqual(false, updatedSkill.IsCanonical);
            }

            [TestMethod]
            public async Task UpdateSkill_GivenIdOfSkillThatDoesntExist_DoesNotThrow()
            {
                int idOfSkillThatDoesntExist = _fixture.Create<int>();
                var subject = _fixture.Create<SkillManager>();

                await subject.UpdateSkillAsync(idOfSkillThatDoesntExist, "keyword", "skillDescription", false);

                // No exception thrown
            }
        }

        [TestClass]
        public class LoadByName : SkillManagerTests
        {
            [TestMethod]
            public async Task LoadByName_GivenNameThatDoesntExist_ReturnsNull()
            {
                var subject = _fixture.Create<SkillManager>();

                var skill = await subject.LoadByNameAsync("Does Not Exist");

                Assert.IsNull(skill);
            }

            [TestMethod]
            public async Task LoadByName_GivenName_ReturnsSkill()
            {
                InsertSkill("C#", null, false);
                InsertSkill("ASP.NET", null, false);
                var subject = _fixture.Create<SkillManager>();

                var skill = await subject.LoadByNameAsync("ASP.NET");

                Assert.AreEqual("ASP.NET", skill.Name);
            }
        }
    }
}
