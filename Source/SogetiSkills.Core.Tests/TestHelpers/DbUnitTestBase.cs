using Microsoft.VisualStudio.TestTools.UnitTesting;
using SogetiSkills.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using SogetiSkills.Core.Managers;
using SogetiSkills.Core.DatabaseMigrations;
using System.Data.SqlClient;
using WebMatrix.Data;

namespace SogetiSkills.Core.Tests.TestHelpers
{
    public class DbUnitTestBase : UnitTestBase
    {
        protected static Database TestDatabase = null;

        static DbUnitTestBase()
        {            
            string connectionString = ConfigurationManager.ConnectionStrings["SogetiSkills"].ConnectionString;
            SqlDatabaseMigrator migrator = new SqlDatabaseMigrator(connectionString, typeof(UserManager).Assembly, "SogetiSkills.Core.DatabaseMigrations");
            migrator.Migrate();

            TestDatabase = Database.Open("SogetiSkills");
        }

        [TestCleanup]
        public void EmptyDatabase()
        {
            var sqlStatements = new[] {
                "DELETE FROM Resumes",
                "DELETE FROM Consultant_Tag",
                "DELETE FROM Tags",
                "DELETE FROM Users",
                "DBCC CHECKIDENT ('Users', RESEED, 1)",
                "DBCC CHECKIDENT ('Resumes', RESEED, 1)",
                "DBCC CHECKIDENT ('Tags', RESEED, 1)"
            };
            
            foreach(string sqlStatement in sqlStatements)
            {
                TestDatabase.Execute(sqlStatement);                    
            }
        }
    }
}
