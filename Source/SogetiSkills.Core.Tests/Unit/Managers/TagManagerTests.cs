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
    public class TagManagerTests : DbUnitTestBase
    {
        [TestClass]
        public class LoadTagsForConsulant : TagManagerTests
        {
            [TestMethod]
            public async Task LoadTagsForConsultant_GivenConsultantWithNoTags_ReturnsEmptySet()
            {
                int userId = InsertUser(SampleData.Consultant());
                var subject = _fixture.Create<TagManager>();

                var tags = await subject.LoadTagsForConsultantAsync(userId);

                Assert.AreEqual(0, tags.Count());
            }

            [TestMethod]
            public async Task LoadTagsForConsultant_GivenConsultantWithTags_ReturnsTags()
            {
                int userId = InsertUser(SampleData.Consultant());
                var tagCSharp = InsertTag("C#", "C# description", true);
                var tagAspNet = InsertTag("ASP.NET", "ASP.NET description", false);
                InsertConsultantTag(userId, tagCSharp.Id);
                InsertConsultantTag(userId, tagAspNet.Id);

                var subject = _fixture.Create<TagManager>();

                var tags = await subject.LoadTagsForConsultantAsync(userId);

                Assert.AreEqual(2, tags.Count());
            }
        }

        [TestClass]
        public class LoadCanonicalTags : TagManagerTests
        {
            [TestMethod]
            public async Task LoadCanonicalTags_ReturnsOnlyCanonicalTags()
            {
                InsertTag("C#", "C# description", true);
                InsertTag("ASP.NET", "ASP.NET description", false);
                InsertTag("JavaScript", "JavaScript description", true);

                var subject = _fixture.Create<TagManager>();

                var tags = await subject.LoadCanonicalTagsAsync();

                Assert.AreEqual(2, tags.Count());
                Assert.IsTrue(tags.All(x => x.IsCanonical));
            }


            [TestMethod]
            public async Task LoadCanonicalTags_ReturnsTagsOrderedByKeyword()
            {
                InsertTag("C#", "C# description", true);
                InsertTag("ASP.NET", "ASP.NET description", true);
                InsertTag("JavaScript", "JavaScript description", true);

                var subject = _fixture.Create<TagManager>();

                var tags = await subject.LoadCanonicalTagsAsync();

                Assert.AreEqual("ASP.NET", tags.ElementAt(0).Keyword);
                Assert.AreEqual("C#", tags.ElementAt(1).Keyword);
                Assert.AreEqual("JavaScript", tags.ElementAt(2).Keyword);
            }
        }

        [TestClass]
        public class AddCanonicalTag : TagManagerTests
        {
            [TestMethod]
            public async Task AddCanonicalTag_GivenNewTag_Inserts()
            {
                var subject = _fixture.Create<TagManager>();

                await subject.AddCanonicalTagAsync("C#", "C# description");

                int count = (int)TestDatabase.QueryValue("SELECT COUNT(*) FROM Tags WHERE Keyword = @0", "C#");
                Assert.AreEqual(1, count);
            }

            [TestMethod]
            public async Task AddCanonicalTag_GivenKeywordThatAlreadyExists_UpdatesExistingTag()
            {
                var existingTag = InsertTag("C#", "C# description", isCanonical: false);
                var subject = _fixture.Create<TagManager>();

                await subject.AddCanonicalTagAsync("C#", "new C# description");

                dynamic newCanonicalTag = TestDatabase.QuerySingle("SELECT Id, Keyword, SkillDescription, IsCanonical FROM Tags WHERE Id = @0", existingTag.Id);
                Assert.AreEqual(existingTag.Id, newCanonicalTag.Id);
                Assert.AreEqual("C#", newCanonicalTag.Keyword);
                Assert.AreEqual("new C# description", newCanonicalTag.SkillDescription);
                Assert.AreEqual(true, newCanonicalTag.IsCanonical);
            }
        }

        [TestClass]
        public class RemoveCanonicalTag : TagManagerTests
        {
            [TestMethod]
            public async Task RemoveCanonicalTag_GivenCanonicalTag_SetsIsCanonicalToFalse()
            {
                var tag = InsertTag("C#", "C# description", true);

                var subject = _fixture.Create<TagManager>();

                await subject.RemoveCanonicalTagAsync(tag.Id);

                bool isCanonical = TestDatabase.QueryValue("SELECT IsCanonical FROM Tags WHERE Id = @0", tag.Id);
                Assert.IsFalse(isCanonical);
            }
        }

        [TestClass]
        public class UpdateTag : TagManagerTests
        {
            [TestMethod]
            public async Task UpdateTag_GivenAllNewValues_UpdatesTheTag()
            {
                var tag = InsertTag("C#", "C# description", true);

                var subject = _fixture.Create<TagManager>();

                await subject.UpdateTagAsync(tag.Id, "New Keyword", "New Skill Description", false);

                var updatedTag = TestDatabase.QuerySingle("SELECT Keyword, SkillDescription, IsCanonical FROM Tags WHERE Id = @0", tag.Id);
                Assert.AreEqual("New Keyword", updatedTag.Keyword);
                Assert.AreEqual("New Skill Description", updatedTag.SkillDescription);
                Assert.AreEqual(false, updatedTag.IsCanonical);
            }

            [TestMethod]
            public async Task UpdateTag_GivenIdOfTagThatDoesntExist_DoesNotThrow()
            {
                int idForTagThatDoesntExist = _fixture.Create<int>();
                var subject = _fixture.Create<TagManager>();

                await subject.UpdateTagAsync(idForTagThatDoesntExist, "keyword", "skillDescription", false);

                // No exception thrown
            }
        }

        [TestClass]
        public class LoadByKeyword : TagManagerTests
        {
            [TestMethod]
            public async Task LoadByKeyword_GivenKeywordThatDoesntExist_ReturnsNull()
            {
                var subject = _fixture.Create<TagManager>();

                var tag = await subject.LoadByKeywordAsync("Does Not Exist");

                Assert.IsNull(tag);
            }

            [TestMethod]
            public async Task LoadByKeyword_GivenKeyword_ReturnsTag()
            {
                InsertTag("C#", null, false);
                InsertTag("ASP.NET", null, false);
                var subject = _fixture.Create<TagManager>();

                var tag = await subject.LoadByKeywordAsync("ASP.NET");

                Assert.AreEqual("ASP.NET", tag.Keyword);
            }
        }
    }
}
