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
    }
}
