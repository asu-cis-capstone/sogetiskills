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

        public static Resume Resume(int userId)
        {
            return new Resume
            {
                Id = 1,
                UserId = userId,
                Metadata = ResumeMetadata(),
                FileData = new byte[] { 0x0, 0x1, 0x2 }
            };
        }

        public static IEnumerable<Skill> TagList()
        {
            yield return new Skill { Id = 1, Name = "C#", IsCanonical = true, Description = "C# Description" };
            yield return new Skill { Id = 2, Name = "ASP.NET", IsCanonical = true, Description = "ASP.NET Description" };
            yield return new Skill { Id = 3, Name = "JavaScript", IsCanonical = true, Description = "JavaScript Description" };
        }
    }
}
