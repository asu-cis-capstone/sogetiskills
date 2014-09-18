using Moq;
using SogetiSkills.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Ploeh.AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.TestHelper;
using SogetiSkills.UI.ViewModels.Profile.UploadResume;

namespace SogetiSkills.Tests.Unit.UI.ViewModels.Profile.UploadResume
{
    [TestClass]
    public class UploadResumeViewModelValidatorTests : UnitTestBase
    {
        Mock<HttpPostedFileBase> _fakeFile;

        public UploadResumeViewModelValidatorTests()
        {
            _fakeFile = new Mock<HttpPostedFileBase>();
            _fixture.Inject(_fakeFile);
        }

        [TestMethod]
        public void Validate_GivenANullFile_HasError()
        {
            UploadResumeViewModelValidator subject = _fixture.Create<UploadResumeViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.PostedFile, null as HttpPostedFileBase);
        }

        [TestMethod]
        public void Validate_GivenEmptyFile_HasError()
        {
            _fakeFile.Setup(x => x.ContentLength).Returns(0);
            UploadResumeViewModelValidator subject = _fixture.Create<UploadResumeViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.PostedFile, _fakeFile.Object);
        }

        [TestMethod]
        public void Validate_GivenInvalidFileExtension_HasError()
        {
            _fakeFile.Setup(x => x.ContentLength).Returns(1024);
            _fakeFile.Setup(x => x.FileName).Returns("InvalidFileName.cs");
            UploadResumeViewModelValidator subject = _fixture.Create<UploadResumeViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.PostedFile, _fakeFile.Object);
        }

        [TestMethod]
        public void Validate_GivenLargeFile_HasError()
        {
            _fakeFile.Setup(x => x.ContentLength).Returns(1024 * 1024 * 13);
            _fakeFile.Setup(x => x.FileName).Returns("ValidFileName.pdf");
            UploadResumeViewModelValidator subject = _fixture.Create<UploadResumeViewModelValidator>();

            subject.ShouldHaveValidationErrorFor(x => x.PostedFile, _fakeFile.Object);
        }

        [TestMethod]
        public void Validate_GivenValidFile_DoesNotHaveError()
        {
            _fakeFile.Setup(x => x.ContentLength).Returns(1024);
            _fakeFile.Setup(x => x.FileName).Returns("ValidFileName.pdf");
            UploadResumeViewModelValidator subject = _fixture.Create<UploadResumeViewModelValidator>();

            subject.ShouldNotHaveValidationErrorFor(x => x.PostedFile, _fakeFile.Object);
        }
    }
}
