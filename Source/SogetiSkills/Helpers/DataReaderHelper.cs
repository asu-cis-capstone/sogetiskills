using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SogetiSkills.Helpers
{
    public static class DataReaderHelper
    {
        public static T CastTo<T>(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return default(T);
            }
            else
            {
                return (T)value;
            }
        }

        public static T Field<T>(this SqlDataReader reader, string columnName)
        {
            object columnValue = reader[columnName];
            return CastTo<T>(columnValue);
        }
    }
}
