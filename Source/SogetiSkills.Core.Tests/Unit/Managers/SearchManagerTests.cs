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
    public class SearchManagerTests : DbUnitTestBase
    {
        [TestClass]
        public class SearchConsultants : SearchManagerTests
        {
            public SearchConsultants()
            {
                // Set up sample data for all tests.
                var cSharp = InsertSkill("C#", true);
                var aspNet = InsertSkill("ASP.NET", true);
                var jugglingOranges = InsertSkill("Juggling Oranges", false);
                InsertUser(SampleData.AccountExecutive());

                int consultant1Id = InsertUser(SampleData.Consultant(emailAddress: "con1@site.com", lastName: "Jenkins", isOnBeach: true));
                InsertConsultantSkill(consultant1Id, cSharp.Id);
                InsertConsultantSkill(consultant1Id, aspNet.Id);

                int consultant2Id = InsertUser(SampleData.Consultant(emailAddress: "con2@site.com", lastName: "Smith", isOnBeach: false));
                InsertConsultantSkill(consultant2Id, jugglingOranges.Id);
            }

            [TestMethod]
            public async Task SearchConsultants_GivenNoInput_ReturnsAllConsultants()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, null, null, null);

                    Assert.AreEqual(2, consultants.Count());
                }
            }
        }

        [TestClass]
        public class SearchConsultantsByBeachStatus : SearchConsultants
        {
            [TestMethod]
            public async Task SearchConsultants_GivenBeachStatus_FiltersOnBeachStatus()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(true, null, null, null);

                    Assert.AreEqual(1, consultants.Count());
                    Assert.IsTrue(consultants.Single().IsOnBeach);
                }
            }
        }


        [TestClass]
        public class SearchConsultantsByLastName : SearchConsultants
        {
            [TestMethod]
            public async Task SearchConsultants_GivenLastName_FiltersOnLastName()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, "Jenkins", null, null);

                    Assert.AreEqual("Jenkins", consultants.Single().LastName);
                }
            }

            [TestMethod]
            public async Task SearchConsultants_GivenLastName_IsCaseInsensitive()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, "JeNkINS", null, null);

                    Assert.AreEqual("Jenkins", consultants.Single().LastName);
                }
            }

            [TestMethod]
            public async Task SearchConsultants_GivenLastName_FiltersWhereLastNameContainsInput()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, "kins", null, null);

                    Assert.AreEqual("Jenkins", consultants.Single().LastName);
                }
            }

            [TestMethod]
            public async Task SearchConsultants_GivenLastNameThatDoesntEixt_ReturnsNoResults()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, "does not exist", null, null);

                    Assert.AreEqual(0, consultants.Count());
                }
            }
        }

        [TestClass]
        public class SearchConsultantsByEmailAddress : SearchConsultants
        {
            [TestMethod]
            public async Task SearchConsultants_GivenEmailAddress_FiltersOnEmailAddress()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, null, "con1@site.com", null);

                    Assert.AreEqual("con1@site.com", consultants.Single().EmailAddress);
                }
            }

            [TestMethod]
            public async Task SearchConsultants_GivenEmailAddress_IsCaseInsensitive()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, null, "cON1@sitE.cOm", null);

                    Assert.AreEqual("con1@site.com", consultants.Single().EmailAddress);
                }
            }

            [TestMethod]
            public async Task SearchConsultants_GivenEmailAddress_FitlersForAnExactMatch()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, "con1", null, null);

                    Assert.AreEqual(0, consultants.Count());
                }
            }

            [TestMethod]
            public async Task SearchConsultants_GivenEmailAddressThatDoesntEixt_ReturnsNoResults()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, null, "does not exist", null);

                    Assert.AreEqual(0, consultants.Count());
                }
            }
        }

        public class SearchConsultantsBySkills : SearchConsultants
        {
            [TestMethod]
            public async Task SearchConsultants_GivenSkills_FiltersBySkills()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, null, null, new[] { "C#", "Juggling Oranges" } );

                    Assert.AreEqual(2, consultants.Count());
                }
            }

            [TestMethod]
            public async Task SearchConsultants_GivenSkills_IsCaseInsensitive()
            {
                using (var subject = _fixture.Create<SearchManager>())
                {
                    var consultants = await subject.SearchConsultantsAsync(null, null, null, new[] { "c#", "JuGGling OranGES" });

                    Assert.AreEqual(2, consultants.Count());
                }
            } 
        }
    }
}
