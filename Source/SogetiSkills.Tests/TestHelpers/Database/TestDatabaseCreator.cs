using SogetiSkills.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Tests.TestHelpers.Database
{
    public static class TestDatabaseCreator
    {
        public static void Create()
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
                foreach (string statement in createDatabaseScript.Split(';'))
                {
                    if (!string.IsNullOrWhiteSpace(statement))
                    {
                        string sqlceCompatibleStatement = MakeSqlCeCompatible(statement);
                        try
                        {
                            var command = new SqlCeCommand(MakeSqlCeCompatible(statement), connection);
                            command.ExecuteNonQuery();
                        }
                        catch (SqlCeException ex)
                        {
                            throw new Exception(sqlceCompatibleStatement, ex);
                        }
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

        private static string MakeSqlCeCompatible(string statement)
        {
            string sqlceCompatibleStatement = statement;

            // SQL CE doesn't allow including columns on an index so we remove the INCLUDES part of
            // a create index statement if it exists.
            if (sqlceCompatibleStatement.Contains("CREATE INDEX"))
            {
                int indexOfIncludesKeyword = sqlceCompatibleStatement.IndexOf("INCLUDE");
                if (indexOfIncludesKeyword != -1)
                {
                    sqlceCompatibleStatement = sqlceCompatibleStatement.Substring(0, indexOfIncludesKeyword);
                }
            }

            // SQL CE doesn't allow columns of length "max".
            sqlceCompatibleStatement = sqlceCompatibleStatement.Replace("max", "4000");

            return sqlceCompatibleStatement;
        }
    }
}
