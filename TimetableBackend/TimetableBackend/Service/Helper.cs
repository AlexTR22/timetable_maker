using System.Data;
using Microsoft.Data.SqlClient;

namespace TimetableBackend.Service
{
    public class Helper
    {
        private static string _connectionString;

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public Helper(string connectionString)
        {
            _connectionString = connectionString;
        }
       
    }
}
