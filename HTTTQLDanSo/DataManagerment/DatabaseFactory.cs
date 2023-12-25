using System.Data.Common;
using System.Data.SqlClient;

namespace HTTTQLDanSo.DataManagerment
{
    public class DatabaseFactory : IDatabaseFactory
    {
        //private readonly IConfiguration _configuration;
        //public DatabaseFactory(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public DatabaseFactory()
        {
        }

        public DbConnection GetDbConnection(string connectionName)
        {
            var connectionString = System.Configuration.ConfigurationManager.AppSettings[connectionName];
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}