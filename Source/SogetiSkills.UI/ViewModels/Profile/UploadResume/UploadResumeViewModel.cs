using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SogetiSkills.UI.ViewModels.Profile.UploadResume
{
    [Validator(typeof(UploadResumeViewModelValidator))]
    public class UploadResumeViewModel
    {
        public int UserId { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
    }

    public class UploadResumeViewModelValidator : AbstractValidator<UploadResumeViewModel>
    {
        public UploadResumeViewModelValidator()
        {
            RuleFor(x => x.PostedFile)
                .Must(NotBeAnEmptyFile).WithMessage("Please select a file.")
                .Must(BeAPdfOrWordDocument).WithMessage("Only PDFs and Microsoft Word documents are accepted.")
                .Must(BeLessThan12Megabytes).WithMessage("File must be less than 12MB.");
        }

        public bool NotBeAnEmptyFile(HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }

        public bool BeAPdfOrWordDocument(HttpPostedFileBase file)
        {
            string extension = Path.GetExtension(file.FileName).ToLower();
            var validExtensions = new[] { ".pdf", ".doc", ".docx" };
            return validExtensions.Contains(extension);
        }

        public bool BeLessThan12Megabytes(HttpPostedFileBase file)
        {
            return file.ContentLength <= 1024 * 1024 * 12;
        }
    }
}