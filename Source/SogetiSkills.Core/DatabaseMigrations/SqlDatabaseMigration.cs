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

        public SqlDatabaseMigration(long migrationId, string name, string script)
        {
            MigartionId = migrationId;
            Name = name;
            Script = script;
        }

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
