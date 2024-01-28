using System.Data.SqlClient;

namespace SolarEcoBackEnd.DB
{
    using Microsoft.Extensions.Configuration;
    using System.Data.SqlClient;

    public class DBconnector
    {
        private readonly IConfiguration _config;
        private SqlConnection _connection;

        private static readonly DBconnector instance = new DBconnector();

        private DBconnector()
        {
            
        }

        private DBconnector(IConfiguration config)
        {
            _config = config;
        }

        public static DBconnector GetInstance(IConfiguration config = null)
        {
            if (config != null)
            {
                return new DBconnector(config);
            }
            return instance;
        }

        public SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                if (_config == null)
                {
                    throw new InvalidOperationException("Configuration is not set.");
                }
                string connectionString = _config.GetConnectionString("Default");
                _connection = new SqlConnection(connectionString);
            }
            return _connection;
        }
    }


}
