using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using SogetiSkills.Helpers;

namespace SogetiSkills.DatabaseMigrations
{
    public class SqlDatabaseMigrator
    {
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly Assembly _migrationScriptsAssembly;
        private readonly string _migrationScriptsNamespace;

        public SqlDatabaseMigrator(string connectionString, Assembly migrationScriptsAssembly, string migrationScriptsNamespace)
        {
            _connectionString = connectionString;
            _migrationScriptsAssembly = migrationScriptsAssembly;
            _migrationScriptsNamespace = migrationScriptsNamespace;
            _databaseName = ExtractDatabaseNameFromConnectionString(connectionString);
        }

        private string ExtractDatabaseNameFromConnectionString(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            return connectionStringBuilder.DataSource;
        }

        public void Migrate()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                EnsureMigrationHistoryTableExists(connection);
                long? mostRecentlyAppliedMigration = GetMostRecentlyAppliedMigration(connection);
                var pendingMigrations = GetPendingMigrations(mostRecentlyAppliedMigration);
                foreach(var migration in pendingMigrations)
                {
                    migration.Apply(connection);
                }
            }
        }

        private void EnsureMigrationHistoryTableExists(SqlConnection connection)
        {
            bool migrationHistoryTableAlreadyExists = MigrationHistoryTableAlreadyExists(connection);
            if (!migrationHistoryTableAlreadyExists)
            {
                CreateMigrationHistoryTable(connection);
            }
        }

        private bool MigrationHistoryTableAlreadyExists(SqlConnection connection)
        {
            string selectDatabaseCountStatement = "SELECT COUNT(*) FROM sys.Tables WHERE Name = '__MigrationHistory'";
            SqlCommand command = new SqlCommand(selectDatabaseCountStatement, connection);
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }

        private void CreateMigrationHistoryTable(SqlConnection connection)
        {
            string createMigrationHistoryTableSatement = @"CREATE TABLE __MigrationHistory
                                                           (
                                                               MigrationId bigint NOT NULL PRIMARY KEY,
                                                               Name nvarchar(MAX) NOT NULL,
                                                               Script nvarchar(MAX) NOT NULL,
                                                               DateAppliedUtc datetime NOT NULL DEFAULT GETUTCDATE()
                                                           );";
            SqlCommand command = new SqlCommand(createMigrationHistoryTableSatement, connection);
            command.ExecuteNonQuery();
        }

        private long? GetMostRecentlyAppliedMigration(SqlConnection connection)
        {
            string selectMostRecentlyAppliedMigrationStatement = "SELECT MAX(MigrationId) FROM __MigrationHistory";
            SqlCommand command = new SqlCommand(selectMostRecentlyAppliedMigrationStatement, connection);
            long? mostRecentlyAppliedMigration = DataReaderHelper.CastTo<long?>(command.ExecuteScalar());
            return mostRecentlyAppliedMigration;
        }

        private IEnumerable<SqlDatabaseMigration> GetPendingMigrations(long? mostRecentlyAppliedMigration)
        {
            mostRecentlyAppliedMigration = mostRecentlyAppliedMigration ?? 0;
            var resourceStreamNames = _migrationScriptsAssembly
                .GetManifestResourceNames()
                .Where(x => x.StartsWith(_migrationScriptsNamespace) && x.EndsWith(".sql"));

            List<SqlDatabaseMigration> migrations = new List<SqlDatabaseMigration>();
            foreach(string resourceStreamName in resourceStreamNames)
            {
                migrations.Add(CreateMigrationFromResourceStream(resourceStreamName));
            }
            return migrations.Where(x => x.MigartionId > mostRecentlyAppliedMigration).ToList();
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
    }
}
