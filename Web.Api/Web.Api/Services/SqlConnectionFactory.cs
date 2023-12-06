using Microsoft.Data.SqlClient;

namespace Web.Api.Services
{
    public class SqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connecctionString)
        {
            _connectionString = connecctionString;
        }
        public SqlConnectionFactory Create()
        {
            return new SqlConnectionFactory(_connectionString)
        }
    }
}
