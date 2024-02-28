using Dapper;
using SolarEcoBackEnd.Entity;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Cryptography;
using SolarEcoBackEnd.DataAcessLayer.Interfaces;
using AltaVision.Logger;
using Microsoft.Extensions.Logging;

namespace SolarEcoBackEnd.DB
{
    public class CustomerDB:ICustomerDB
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();

        private IdFactory factory = new IdFactory();
        private readonly ILogger<CustomerDB> _logger;
        private ILogs _logs;
        public CustomerDB(ILogger<CustomerDB> logger, ILogs logs)
        {
            _logger = logger;
            _logs = logs;
        }


        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            var getadmin = await connection.QueryAsync<Customer>("select * from Customer");

            return getadmin;

        }
        public async Task<Customer> GetCustomerbyId(string customerid)
        {
            var customer = await connection.QueryFirstAsync<Customer>("select * from Customer where CustomerId= @id", new { id = customerid });
            return customer;

        }
        public async Task<Customer> CustomerLogin(string Email, string Password)
        {
            var customer = await connection.QuerySingleOrDefaultAsync<Customer>("select * from Customer where Email= @email and Password=@password", new { email = Email, password=Crypto.Encrypt(Password) });
            
            return customer;

        }
        public async Task<int> RegisterCustomer(Customer customer)
        {
            Id id = factory.CreateId("Customer");
            customer.CustomerId = id.getId();
            customer.Password=Crypto.Encrypt(customer.Password);
            int result = await connection.ExecuteAsync("Insert into Customer (CustomerId, FirstName, LastName,DOB, MobileNo,Address, Email, Password, CreatedDate, StatusId) values(@CustomerId, @FirstName, @LastName,@DOB,@MobileNo,@Address, @Email, @Password, @CreatedDate, @StatusId)", customer);

            return result;

        }
        public async Task<string> GetCustomerid()
        {
            Id id = factory.CreateId("Customer");
            string adminid = id.getId();
            return adminid;

        }
    }
}
