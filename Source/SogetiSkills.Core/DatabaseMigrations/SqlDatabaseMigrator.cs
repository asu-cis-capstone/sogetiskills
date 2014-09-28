using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using SogetiSkills.Core.Helpers;

namespace SogetiSkills.Core.DatabaseMigrations
{
    public class SqlDatabaseMigrator
    {
        private readonly string _connectionString;
        private readonly Assembly _migrationScriptsAssembly;
        private readonly string _migrationScriptsNamespace;

        public SqlDatabaseMigrator(string connectionString, Assembly migrationScriptsAssembly, string migrationScriptsNamespace)
        {
            _connectionString = connectionString;
            _migrationScriptsAssembly = migrationScriptsAssembly;
            _migrationScriptsNamespace = migrationScriptsNamespace;
        }

        public void Migrate()
        {
            EnsureDatabaseExists();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                EnsureMigrationHistoryTableExists(connection);
                ApplyMigrations(connection);
            }
        }

        #region Ensure database exists
        private void EnsureDatabaseExists()
        {
            string connectionString = CreateConnectionStringWithoutDatabaseName();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string createDatabaseStatement = @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{0}')
	                                           BEGIN
	                                           	  CREATE DATABASE {0}
	                                           END";
                string databseName = ExtractDatabaseNameFromConnectionString();
                createDatabaseStatement = string.Format(createDatabaseStatement, databseName);
                
                connection.Open();
                SqlCommand command = new SqlCommand(createDatabaseStatement, connection);
                command.ExecuteNonQuery();
            }
        }

        private string CreateConnectionStringWithoutDatabaseName()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            connectionStringBuilder.Remove("Database");
            return connectionStringBuilder.ConnectionString;
        }

        private string ExtractDatabaseNameFromConnectionString()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            return connectionStringBuilder.InitialCatalog;
        }
        #endregion

        #region Ensure migrations history table exists
        private void EnsureMigrationHistoryTableExists(SqlConnection connection)
        {
            string createMigrationHistoryTableSatement =
                @"IF NOT EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_Name = '__MigrationHistory')
	              BEGIN
		              CREATE TABLE __MigrationHistory
                      (
                          MigrationId bigint NOT NULL PRIMARY KEY,
                          Name nvarchar(MAX) NOT NULL,
                          Script nvarchar(MAX) NOT NULL,
                          DateAppliedUtc datetime NOT NULL DEFAULT GETUTCDATE()
                      );
	              END";
            SqlCommand command = new SqlCommand(createMigrationHistoryTableSatement, connection);
            command.ExecuteNonQuery();
        }
        #endregion

        #region Apply migrations
        private void ApplyMigrations(SqlConnection connection)
        {
            IEnumerable<long> alreadyAppliedMigrations = GetAlreadyAppliedMigrations(connection);
            var pendingMigrations = GetPendingMigrations(alreadyAppliedMigrations);
            foreach (var migration in pendingMigrations)
            {
                migration.Apply(connection);
            }
        }

        private IEnumerable<long> GetAlreadyAppliedMigrations(SqlConnection connection)
        {
            string selectMostRecentlyAppliedMigrationStatement = "SELECT MigrationId FROM __MigrationHistory";
            SqlCommand command = new SqlCommand(selectMostRecentlyAppliedMigrationStatement, connection);
            List<long> appliedMigrations = new List<long>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    appliedMigrations.Add(reader.Field<long>("MigrationId"));
                }
            }
            return appliedMigrations;
        }

        private IEnumerable<SqlDatabaseMigration> GetPendingMigrations(IEnumerable<long> appliedMigrations)
        {
            var resourceStreamNames = _migrationScriptsAssembly
                .GetManifestResourceNames()
                .Where(x => x.StartsWith(_migrationScriptsNamespace) && x.EndsWith(".sql"));

            List<SqlDatabaseMigration> migrations = new List<SqlDatabaseMigration>();
            foreach (string resourceStreamName in resourceStreamNames)
            {
                migrations.Add(CreateMigrationFromResourceStream(resourceStreamName));
            }
            return migrations
                .Where(x => !appliedMigrations.Contains(x.MigartionId))
                .OrderBy(x => x.MigartionId)
                .ToList();
        }

        private SqlDatabaseMigration CreateMigrationFromResourceStream(string resourceStreamName)
        {
            string resourceStreamNameWithoutExtension = resourceStreamName.Replace(".sql", string.Empty);
            int lastDotIndex = resourceStreamNameWithoutExtension.LastIndexOf('.');
            string fileName = resourceStreamNameWithoutExtension.Substring(lastDotIndex + 1);
            long migrationId = long.Parse(new string(fileName.TakeWhile(x => char.IsDigit(x)).ToArray()));
            string name = new string(fileName.SkipWhile(x => char.IsDigit(x) || x == '_').ToArray());
            string script = null;
            using (StreamReader reader = new StreamReader(_migrationScriptsAssembly.GetManifestResourceStream(resourceStreamName)))
            {
                script = reader.ReadToEnd();
            }
            return new SqlDatabaseMigration(migrationId, name, script);
        }
        #endregion
    }
}
