using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SogetiSkills.UI.Tests.TestHelpers
{
    public static class SampleData
    {
        private static Random random = new Random();

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

        public static IEnumerable<Skill> CanonicalSkillList()
        {
            return new List<Skill> {
                new Skill { Id = 1, Name = "C#", IsCanonical = true },
                new Skill { Id = 2, Name = "ASP.NET", IsCanonical = true },
                new Skill { Id = 3, Name = "JavaScript", IsCanonical = true }
            };
        }

        public static IEnumerable<ConsultantSkill> ConsultantSkillList(int consultantId)
        {
            return new List<ConsultantSkill> {
                new ConsultantSkill { ConsultantId = consultantId, SkillId = 1, SkillName = "C#", IsCanonical = true, Proficiency = new  ProficiencyLevel { Name = "Intermediate", Level  = 3 } },
                new ConsultantSkill { ConsultantId = consultantId, SkillId = 1, SkillName = "ASP.NET", IsCanonical = false, Proficiency = new  ProficiencyLevel { Name = "Advanced", Level  = 4 } },
                new ConsultantSkill { ConsultantId = consultantId, SkillId = 1, SkillName = "JavaScript", IsCanonical = true, Proficiency = new  ProficiencyLevel { Name = "Intermediate", Level  = 3 } }
            };
        }

        public static int RandomNumber(int lower, int upper)
        {
            return random.Next(lower, upper);
        }

        public static IEnumerable<string> RandomSkillNames(int count)
        {
            return (from x in File.ReadAllLines("SampleSkillNames.txt")
                    where !string.IsNullOrWhiteSpace(x)
                    orderby Guid.NewGuid()
                    select x).Take(count).ToList();
        }
    }
}
