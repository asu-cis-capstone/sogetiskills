using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Core.Managers
{
    public abstract class ManagerBase : IDisposable
    {
        private SqlConnection _connection = null;

        protected SqlConnection GetOpenConnection()
        {
            GetConnection();
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            return _connection;
        }

        protected async Task<SqlConnection> GetOpenConnectionAsync()
        {
            GetConnection();
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                await _connection.OpenAsync();
            }
            return _connection;
        }

        private SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SogetiSkills"].ConnectionString;
                _connection = new SqlConnection(connectionString);
            }
            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
