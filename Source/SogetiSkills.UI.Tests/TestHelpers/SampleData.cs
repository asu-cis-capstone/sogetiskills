using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.UI.Tests.TestHelpers
{
    public static class SampleData
    {
        public static Consultant Consultant()
        {
            return new Consultant
            {
                Id = 123,
                EmailAddress = "bill@site.com",
                FirstName = "Bill",
                LastName = "Smith",
                IsOnBeach = true,
                PhoneNumber = new PhoneNumber("1234567890")
            };
        }

        public static AccountExecutive AccountExecutive()
        {
            return new AccountExecutive
            {
                Id = 456,
                EmailAddress = "pedro@site.com",
                FirstName = "Pedro",
                LastName = "Garcia",
                PhoneNumber = new PhoneNumber("0987654321")
            };
        }

        public static ResumeMetadata ResumeMetadata()
        {
            return new ResumeMetadata
            {
                FileName = "Bill_Smith_Resume.pdf",
                MimeType = "application/pdf"
            };
        }

        public static IEnumerable<Tag> TagList()
        {
            yield return new Tag { Id = 1, Keyword = "C#", IsCanonical = true, SkillDescription = "C# Description" };
            yield return new Tag { Id = 2, Keyword = "ASP.NET", IsCanonical = true, SkillDescription = "ASP.NET Description" };
            yield return new Tag { Id = 3, Keyword = "JavaScript", IsCanonical = true, SkillDescription = "JavaScript Description" };
        }
    }
}
