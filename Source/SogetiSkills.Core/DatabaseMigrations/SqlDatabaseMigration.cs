using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.DatabaseMigrations
{
    public class SqlDatabaseMigration
    {
        public long MigartionId { get; private set; }
        public string Name { get; private set; }
        public string Script { get; private set; }

        /// <summary>
        /// Represents a single migration to be made to the database.
        /// </summary>
        /// <param name="migrationId">The id of the migration.  It should be the DateTime.UtcNow.Ticks of when 
        /// the migration script was created.</param>
        /// <param name="name">The friendly name of the migration script.</param>
        /// <param name="script">The actual SQL script to execute when the migration is run.</param>
        public SqlDatabaseMigration(long migrationId, string name, string script)
        {
            MigartionId = migrationId;
            Name = name;
            Script = script;
        }

        /// <summary>
        /// Actually apply the migration by executing the migration script against the database.
        /// </summary>
        /// <param name="connection">An open connection to the database to be migrated.</param>
        public void Apply(SqlConnection connection)
        {
            ApplyMigrationScript(connection);
            InsertMigrationHistoryRecord(connection);
        }

        private void ApplyMigrationScript(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(Script, connection);
            command.ExecuteNonQuery();
        }

        private void InsertMigrationHistoryRecord(SqlConnection connection)
        {
            string insertMigrationHistoryRecordStatement = "INSERT INTO __MigrationHistory (MigrationId, Name, Script) VALUES (@migrationId, @name, @script)";
            SqlCommand command = new SqlCommand(insertMigrationHistoryRecordStatement, connection);
            command.Parameters.AddWithValue("@migrationId", MigartionId);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@script", Script);
            command.ExecuteNonQuery();
        }
    }
}
