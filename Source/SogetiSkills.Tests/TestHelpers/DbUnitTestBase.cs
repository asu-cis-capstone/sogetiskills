using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Models;
using SogetiSkills.Tests.TestHelpers.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlServerCe;
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
            TestDatabaseCreator.Create();
        }
       
        [TestCleanup]
        public void EmptyDatabase()
        {
            TestDatabaseDeleter.EmptyDatabase();
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
