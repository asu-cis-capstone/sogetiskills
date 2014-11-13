using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Tests.TestHelpers
{
    public static class SampleData
    {
        public static Consultant Consultant(
            int id = 123, 
            string emailAddress = "bill@site.com",
            string firstName = "Bill",
            string lastName = "Smith",
            bool isOnBeach = false,
            string phoneNumber = "1234567890")
        {
            return new Consultant
            {
                Id = id,
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                IsOnBeach = isOnBeach,
                PhoneNumber = new PhoneNumber(phoneNumber),
                Password = new HashedPassword
                {
                    Hash = "hash",
                    Salt = "salt"
                }
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
                PhoneNumber = new PhoneNumber("0987654321"),
                Password = new HashedPassword
                {
                    Hash = "hash",
                    Salt = "salt"
                }
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
            yield return new Skill { Id = 1, Name = "C#", IsCanonical = true };
            yield return new Skill { Id = 2, Name = "ASP.NET", IsCanonical = true };
            yield return new Skill { Id = 3, Name = "JavaScript", IsCanonical = true };
        }
    }
}
