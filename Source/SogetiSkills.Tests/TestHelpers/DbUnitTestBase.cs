using SogetiSkills.Models;
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

namespace SogetiSkills.Tests.TestHelpers
{
    public class DbUnitTestBase : UnitTestBase
    {
        static DbUnitTestBase()
        {
            RecreateSqlCeDatabaseFile();
            CreateTableStructure();
        }

        private static void RecreateSqlCeDatabaseFile()
        {
            System.IO.File.Delete("SogetiSkillsTest.sdf");

            string connectionString = ConfigurationManager.ConnectionStrings["SogetiSkills"].ConnectionString;
            using (var engine = new SqlCeEngine(connectionString))
            {
                engine.CreateDatabase();
            }
        }

        private static void CreateTableStructure()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SogetiSkills"].ConnectionString;
            using (var connection = new SqlCeConnection(connectionString))
            {
                string createDatabaseScript = ExtractCreateDatabaseScript(typeof(SogetiSkillsDataContext).Assembly);
                connection.Open();
                foreach(string statement in createDatabaseScript.Split(';'))
                {
                    if (!string.IsNullOrWhiteSpace(statement))
                    {
                        var command = new SqlCeCommand(statement, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        
        private static string ExtractCreateDatabaseScript(Assembly sogetiSkillsAssembly)
        {
            using (Stream reasourceStream = sogetiSkillsAssembly.GetManifestResourceStream("SogetiSkills.Models.CreateDatabase.sql"))
            using (StreamReader streamReader = new StreamReader(reasourceStream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
