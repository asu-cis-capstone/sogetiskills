using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Tests.TestHelpers.Database
{
    public static class TestDatabaseDeleter
    {
        public static void EmptyDatabase()
        {
            using (var db = new SogetiSkillsDataContext())
            {
                db.Users.RemoveRange(db.Users);
                db.Resumes.RemoveRange(db.Resumes);
                db.Tags.RemoveRange(db.Tags);
                db.SaveChanges();
            }
        }
    }
}
