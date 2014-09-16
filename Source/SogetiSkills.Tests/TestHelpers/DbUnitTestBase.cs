using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;

namespace SogetiSkills.Tests.TestHelpers
{
    public class DbUnitTestBase : UnitTestBase
    {
        static DbUnitTestBase()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ""));
        }

        [TestCleanup]
        public void EmptyDatabase()
        {
            using (var db = new SogetiSkillsDataContext())
            {
                db.Users.RemoveRange(db.Users);
                db.Resumes.RemoveRange(db.Resumes);
                db.Tags.RemoveRange(db.Tags);
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Users', RESEED, 1)");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Resumes', RESEED, 1)");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Tags', RESEED, 1)");
            }
        }

        protected SogetiSkillsDataContext DataContext
        {
            get
            {
                return _fixture.Create<SogetiSkillsDataContext>();
            }
        }
    }
}
