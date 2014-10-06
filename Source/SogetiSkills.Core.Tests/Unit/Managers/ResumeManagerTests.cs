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
    public class ResumeManagerTests : DbUnitTestBase
    {
        [TestClass]
        public class LoadResumeMetadataByUserId : ResumeManagerTests
        {
            [TestMethod]
            public async Task LoadResumeMetadataByUserId_GivenIdOfUserThatDoesExist_ReturnsNull()
            {
                var subject = _fixture.Create<ResumeManager>();
                int idOfUserThatDoesntExist = 123456;

                var resumeMetadata = await subject.LoadResumeMetadataByUserIdAsync(idOfUserThatDoesntExist);
                
                Assert.IsNull(resumeMetadata);
            }

            [TestMethod]
            public async Task LoadResumeMetadataByUserId_GivenIdOfUserThatHasNoResume_ReturnsNull()
            {
                int userId = InsertUser(SampleData.Consultant());
                var subject = _fixture.Create<ResumeManager>();

                var resumeMetadata = await subject.LoadResumeMetadataByUserIdAsync(userId);
                
                Assert.IsNull(resumeMetadata);
            }

            [TestMethod]
            public async Task LoadResumeMetadataByUserId_GivenIdOfUserWithResume_ReturnsResumeMetadata()
            {
                int userId = InsertUser(SampleData.Consultant());
                var expected = SampleData.Resume(userId: userId);
                InsertResume(expected);
                var subject = _fixture.Create<ResumeManager>();

                var resumeMetadata = await subject.LoadResumeMetadataByUserIdAsync(userId);

                Assert.AreEqual(expected.Metadata.FileName, resumeMetadata.FileName);
                Assert.AreEqual(expected.Metadata.MimeType, resumeMetadata.MimeType);
            }
        }

        [TestClass]
        public class LoadResumeByUserId : ResumeManagerTests
        {
            [TestMethod]
            public async Task LoadResumeByUserId_GivenIdOfUserThatDoesExist_ReturnsNull()
            {
                var subject = _fixture.Create<ResumeManager>();
                int idOfUserThatDoesntExist = 123456;

                var resumeMetadata = await subject.LoadResumeByUserIdAsync(idOfUserThatDoesntExist);

                Assert.IsNull(resumeMetadata);
            }

            [TestMethod]
            public async Task LoadResumeByUserId_GivenIdOfUserThatHasNoResume_ReturnsNull()
            {
                int uesrId = InsertUser(SampleData.Consultant());
                var subject = _fixture.Create<ResumeManager>();

                var resumeMetadata = await subject.LoadResumeMetadataByUserIdAsync(uesrId);

                Assert.IsNull(resumeMetadata);
            }

            [TestMethod]
            public async Task LoadResumeByUserId_GivenIdOfUserWithResume_ReturnsResumeMetadata()
            {
                int userId = InsertUser(SampleData.Consultant());
                var expected = SampleData.Resume(userId: userId);
                InsertResume(expected);
                var subject = _fixture.Create<ResumeManager>();

                var resume = await subject.LoadResumeByUserIdAsync(userId);

                Assert.AreEqual(expected.UserId, resume.UserId);
                Assert.IsTrue(expected.FileData.SequenceEqual(resume.FileData));
                Assert.AreEqual(expected.Metadata.FileName, resume.Metadata.FileName);
                Assert.AreEqual(expected.Metadata.MimeType, resume.Metadata.MimeType);
            }
        }

        [TestClass]
        public class UploadResume : ResumeManagerTests
        {
            [TestMethod]
            public async Task UploadResume_GivenNewResume_ReplacesExistingResume()
            {
                int userId = InsertUser(SampleData.Consultant());
                var oldResume = SampleData.Resume(userId: userId);
                InsertResume(oldResume);
                string newFileName = "brand_new_resume.pdf";
                var subject = _fixture.Create<ResumeManager>();

                await subject.UploadResumeAsync(userId, newFileName, oldResume.Metadata.MimeType, oldResume.FileData);

                string actual = TestDatabase.QueryValue("SELECT FileName FROM Resumes WHERE UserId = @0", userId);
                Assert.AreEqual(newFileName, actual);
            }
        }
    }
}
