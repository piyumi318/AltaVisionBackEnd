using Dapper;
using SolarEcoBackEnd.Entity;
using System.Data.SqlClient;

namespace SolarEcoBackEnd.DB
{
    public class CustomerDB
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();

        private IdFactory factory = new IdFactory();


        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            var getadmin = await connection.QueryAsync<Customer>("select * from Customer");

            return getadmin;

        }
        public async Task<Customer> GetCustomerbyId(int customerid)
        {
            var customer = await connection.QueryFirstAsync<Customer>("select * from Customer where CustomerId= @id", new { id = customerid });
            return customer;

        }
        public async Task<int> RegisterCustomer(Customer customer)
        {
            Id id = factory.CreateId("Customer");
            customer.CustomerId = id.getId();
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
