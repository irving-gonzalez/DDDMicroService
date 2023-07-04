using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DDDMicroservice.Infrastructure.DataAccess
{
    public class DapperContext
    {
        private readonly string _connectionString;
        public DapperContext(ConfigurationManager configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString("Member");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}