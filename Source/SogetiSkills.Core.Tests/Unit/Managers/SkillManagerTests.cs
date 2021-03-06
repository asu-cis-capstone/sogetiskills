﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                using (var subject = _fixture.Create<SkillManager>())
                {
                    var skills = await subject.LoadSkillsForConsultantAsync(userId);

                    Assert.AreEqual(0, skills.Count());
                }
            }

            [TestMethod]
            public async Task LoadSkillsForConsultant_GivenConsultantWithSkills_ReturnsSkills()
            {
                int userId = InsertUser(SampleData.Consultant());
                var cSharp = InsertSkill("C#", true);
                var aspNet = InsertSkill("ASP.NET", false);
                InsertConsultantSkill(userId, cSharp.Id);
                InsertConsultantSkill(userId, aspNet.Id);

                using (var subject = _fixture.Create<SkillManager>())
                {
                    var skills = await subject.LoadSkillsForConsultantAsync(userId);

                    Assert.AreEqual(2, skills.Count());
                }
            }
        }

        [TestClass]
        public class LoadCanonicalSkills : SkillManagerTests
        {
            [TestMethod]
            public async Task LoadCanonicalSkills_ReturnsOnlyCanonicalSkills()
            {
                InsertSkill("C#", true);
                InsertSkill("ASP.NET", false);
                InsertSkill("JavaScript", true);

                using (var subject = _fixture.Create<SkillManager>())
                {
                    var skills = await subject.LoadCanonicalSkillsAsync();

                    Assert.AreEqual(2, skills.Count());
                    Assert.IsTrue(skills.All(x => x.IsCanonical));
                }
            }


            [TestMethod]
            public async Task LoadCanonicalSkills_ReturnsSkillsOrderedByKeyword()
            {
                InsertSkill("C#", true);
                InsertSkill("ASP.NET", true);
                InsertSkill("JavaScript", true);

                using (var subject = _fixture.Create<SkillManager>())
                {
                    var skills = await subject.LoadCanonicalSkillsAsync();

                    Assert.AreEqual("ASP.NET", skills.ElementAt(0).Name);
                    Assert.AreEqual("C#", skills.ElementAt(1).Name);
                    Assert.AreEqual("JavaScript", skills.ElementAt(2).Name);
                }
            }
        }

        [TestClass]
        public class AddCanonicalSkill : SkillManagerTests
        {
            [TestMethod]
            public async Task AddCanonicalSkill_GivenNewSkill_Inserts()
            {
                using (var subject = _fixture.Create<SkillManager>())
                {
                    await subject.AddCanonicalSkillAsync("C#");

                    int count = (int)TestDatabase.QueryValue("SELECT COUNT(*) FROM Skills WHERE Name = @0", "C#");
                    Assert.AreEqual(1, count);
                }
            }

            [TestMethod]
            public async Task AddCanonicalSkill_GivenNameThatAlreadyExists_UpdatesExistingSkill()
            {
                var existingSkill = InsertSkill("C#", isCanonical: false);
                using (var subject = _fixture.Create<SkillManager>())
                {
                    await subject.AddCanonicalSkillAsync("C#");

                    dynamic newCanonicalSkill = TestDatabase.QuerySingle("SELECT Id, Name, IsCanonical FROM Skills WHERE Id = @0", existingSkill.Id);
                    Assert.AreEqual(existingSkill.Id, newCanonicalSkill.Id);
                    Assert.AreEqual("C#", newCanonicalSkill.Name);
                    Assert.AreEqual(true, newCanonicalSkill.IsCanonical);
                }
            }

            [TestMethod]
            public async Task AddCanonicalSkill_GivenNullDescription_Inserts()
            {
                using (var subject = _fixture.Create<SkillManager>())
                {
                    await subject.AddCanonicalSkillAsync("C#");

                    int count = (int)TestDatabase.QueryValue("SELECT COUNT(*) FROM Skills WHERE Name = @0", "C#");
                    Assert.AreEqual(1, count);
                }
            }
        }

        [TestClass]
        public class RemoveCanonicalSkill : SkillManagerTests
        {
            [TestMethod]
            public async Task RemoveCanonicalSkill_GivenCanonicalSkillInUse_SetsIsCanonicalToFalse()
            {
                int userId = InsertUser(SampleData.Consultant());
                var canonicalSkill = InsertSkill("C#", true);
                InsertConsultantSkill(userId, canonicalSkill.Id);

                using (var subject = _fixture.Create<SkillManager>())
                {
                    await subject.RemoveCanonicalSkillAsync(canonicalSkill.Id);

                    bool isCanonical = TestDatabase.QueryValue("SELECT IsCanonical FROM Skills WHERE Id = @0", canonicalSkill.Id);
                    Assert.IsFalse(isCanonical);
                }
            }

            [TestMethod]
            public async Task RemoveCanonicalSkill_GivenCanonicalSkillNotInUse_DeletesSkill()
            {
                var canonicalSkill = InsertSkill("C#", true);

                using (var subject = _fixture.Create<SkillManager>())
                {
                    await subject.RemoveCanonicalSkillAsync(canonicalSkill.Id);

                    int count = TestDatabase.QueryValue("SELECT COUNT(*) FROM Skills WHERE Id = @0", canonicalSkill.Id);
                    Assert.AreEqual(0, count);
                }
            }
        }

        [TestClass]
        public class UpdateSkill : SkillManagerTests
        {
            [TestMethod]
            public async Task UpdateSkill_GivenAllNewValues_UpdatesTheSkill()
            {
                var skill = InsertSkill("C#", true);

                using (var subject = _fixture.Create<SkillManager>())
                {
                    await subject.UpdateSkillAsync(skill.Id, "New Name", false);

                    var updatedSkill = TestDatabase.QuerySingle("SELECT Name, IsCanonical FROM Skills WHERE Id = @0", skill.Id);
                    Assert.AreEqual("New Name", updatedSkill.Name);
                    Assert.AreEqual(false, updatedSkill.IsCanonical);
                }
            }

            [TestMethod]
            public async Task UpdateSkill_GivenIdOfSkillThatDoesntExist_DoesNotThrow()
            {
                int idOfSkillThatDoesntExist = _fixture.Create<int>();
                using (var subject = _fixture.Create<SkillManager>())
                {
                    await subject.UpdateSkillAsync(idOfSkillThatDoesntExist, "keyword", false);
                }

                // No exception thrown
            }
        }

        [TestClass]
        public class LoadByName : SkillManagerTests
        {
            [TestMethod]
            public async Task LoadByName_GivenNameThatDoesntExist_ReturnsNull()
            {
                using (var subject = _fixture.Create<SkillManager>())
                {
                    var skill = await subject.LoadByNameAsync("Does Not Exist");

                    Assert.IsNull(skill);
                }
            }

            [TestMethod]
            public async Task LoadByName_GivenName_ReturnsSkill()
            {
                InsertSkill("C#", false);
                InsertSkill("ASP.NET", false);
                using (var subject = _fixture.Create<SkillManager>())
                {
                    var skill = await subject.LoadByNameAsync("ASP.NET");

                    Assert.AreEqual("ASP.NET", skill.Name);
                }
            }
        }

        [TestClass]
        public class LoadById : SkillManagerTests
        {
            [TestMethod]
            public async Task LoadById_GivenIdThatDoesntExist_ReturnsNull()
            {
                using (var subject = _fixture.Create<SkillManager>())
                {
                    var skill = await subject.LoadByIdAsync(_fixture.Create<int>());

                    Assert.IsNull(skill);
                }
            }

            [TestMethod]
            public async Task LoadById_GivenId_ReturnsSkill()
            {
                var cSharp = InsertSkill("C#", false);
                using (var subject = _fixture.Create<SkillManager>())
                {
                    var skill = await subject.LoadByIdAsync(cSharp.Id);

                    Assert.AreEqual("C#", skill.Name);
                    Assert.AreEqual(cSharp.Id, skill.Id);
                }
            }
        }

        [TestClass]
        public class AddSkillToConsultant : SkillManagerTests
        {
            [TestMethod]
            public async Task AddSkillToConsultant_GivenSkillThatDoesntExist_InsertsSkillAndTiesItToConsultant()
            {
                int consultantId = InsertUser(SampleData.Consultant());
                var subject = _fixture.Create<SkillManager>();

                await subject.AddSkillToConsultantAsync("Brand new skill.", consultantId, 3);

                var skills = await subject.LoadSkillsForConsultantAsync(consultantId);
                Assert.AreEqual(1, skills.Count());
                Assert.AreEqual("Brand new skill.", skills.First().SkillName);
                Assert.AreEqual(3, skills.First().Proficiency.Level);
            }

            [TestMethod]
            public async Task AddSkillToConsultant_GivenSkillTheConsultantAlreadyHas_DoesNotInsertNewManyToManyRecord()
            {
                int consultantId = InsertUser(SampleData.Consultant());
                var skill = InsertSkill("C#", false);
                InsertConsultantSkill(consultantId, skill.Id);
                var subject = _fixture.Create<SkillManager>();

                await subject.AddSkillToConsultantAsync("C#", consultantId, 2);

                var skills = await subject.LoadSkillsForConsultantAsync(consultantId);
                Assert.AreEqual(1, skills.Count());
            }

            [TestMethod]
            public async Task AddSkillToConsultant_GivenSkillThatAlreadyExists_JustTiesItToConsultant()
            {
                int consultantId = InsertUser(SampleData.Consultant());
                var cSharp = InsertSkill("C#", false);
                var subject = _fixture.Create<SkillManager>();

                await subject.AddSkillToConsultantAsync("C#", consultantId, 4);

                var skills = await subject.LoadSkillsForConsultantAsync(consultantId);
                Assert.AreEqual(1, skills.Count());
                Assert.AreEqual("C#", skills.First().SkillName);
                Assert.AreEqual(cSharp.Id, skills.First().SkillId);
                Assert.AreEqual(4, skills.First().Proficiency.Level);
            }
        }

        [TestClass]
        public class RemoveSkillFromConsultant : SkillManagerTests
        {
            [TestMethod]
            public async Task RemoveSkillFromConsultant_GivenSkillTiedToConsultant_RemovesTheManyToManyRecord()
            {
                int consultantId = InsertUser(SampleData.Consultant());
                var skill = InsertSkill("C#", false);
                InsertConsultantSkill(consultantId, skill.Id);
                var subject = _fixture.Create<SkillManager>();

                await subject.RemoveSkillFromConsultantAsync(consultantId, skill.Id);

                var skills = await subject.LoadSkillsForConsultantAsync(consultantId);
                Assert.AreEqual(0, skills.Count());
            }

            [TestMethod]
            public async Task RemoveSkillFromConsultant_GivenNonCanonicalSkillNotTiedToOtherConsultants_RemovesTheSkill()
            {
                int consultantId = InsertUser(SampleData.Consultant());
                var skill = InsertSkill("C#", false);
                InsertConsultantSkill(consultantId, skill.Id);
                var subject = _fixture.Create<SkillManager>();

                await subject.RemoveSkillFromConsultantAsync(consultantId, skill.Id);

                int skillCount = (int)TestDatabase.QueryValue("SELECT COUNT(*) FROM Skills WHERE Id = @0", skill.Id);
                Assert.AreEqual(0, skillCount);
            }

            [TestMethod]
            public async Task RemoveSkillFromConsultant_GivenNonCanonicalSkillTiedToOtherConsultants_DoesNotRemoveTheSkill()
            {
                // Insert two consultants.
                int consultant1Id = InsertUser(SampleData.Consultant());
                var consultant2 = SampleData.Consultant();
                consultant2.EmailAddress = "consultant2@site.com";
                int consultant2Id = InsertUser(consultant2);
                // Insert a skill.
                var skill = InsertSkill("C#", false);
                // Tie the skill to both consultant.s
                InsertConsultantSkill(consultant1Id, skill.Id);
                InsertConsultantSkill(consultant2Id, skill.Id);
                var subject = _fixture.Create<SkillManager>();

                await subject.RemoveSkillFromConsultantAsync(consultant1Id, skill.Id);

                int skillCount = (int)TestDatabase.QueryValue("SELECT COUNT(*) FROM Skills WHERE Id = @0", skill.Id);
                Assert.AreEqual(1, skillCount);
            }

            [TestMethod]
            public async Task RemoveSkillFromConsultant_GivenCanonicalSkillTiedToNoOtherConsultants_DoesNoRemoveTheSkill()
            {
                int consultantId = InsertUser(SampleData.Consultant());
                var skill = InsertSkill("C#", true);
                InsertConsultantSkill(consultantId, skill.Id);
                var subject = _fixture.Create<SkillManager>();

                await subject.RemoveSkillFromConsultantAsync(consultantId, skill.Id);

                int skillCount = (int)TestDatabase.QueryValue("SELECT COUNT(*) FROM Skills WHERE Id = @0", skill.Id);
                Assert.AreEqual(1, skillCount);
            }
        }

        [TestClass]
        public class LoadProficiencyLevels : SkillManagerTests
        {
            [TestMethod]
            public async Task LoadProficiencyLevels_LoadsAllProficiencyLevelsOrderedByLevel()
            {
                var subject = _fixture.Create<SkillManager>();

                var proficiencyLevels = await subject.LoadProficiencyLevelsAsync();

                Assert.AreEqual(5, proficiencyLevels.Count());
                Assert.AreEqual("Fundamental Awareness", proficiencyLevels.ElementAt(0).Name);
                Assert.AreEqual("Novice", proficiencyLevels.ElementAt(1).Name);
                Assert.AreEqual("Intermediate", proficiencyLevels.ElementAt(2).Name);
                Assert.AreEqual("Advanced", proficiencyLevels.ElementAt(3).Name);
                Assert.AreEqual("Expert", proficiencyLevels.ElementAt(4).Name);

                Assert.IsNotNull(proficiencyLevels.ElementAt(0).SecondPersonDescription);
                Assert.IsNotNull(proficiencyLevels.ElementAt(0).ThirdPersonDescription);
            }
        }
    }
}
