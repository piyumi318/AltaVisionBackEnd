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


        public async Task<IEnumerable<Customer>?> GetCustomer()
        {
            try

            {
                var getadmin = await connection.QueryAsync<Customer>("select * from Customer");
                if (getadmin != null)
                {
                    _logger.LogInformation("Customer details are loaded successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Customer details are loaded successfully");
                    return getadmin;
                }
                else
                {
                    _logger.LogWarning("No Customer details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Customer details are available");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured." + ex);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " " + "Error Occured." + ex);
                return null;
            }
           

          

        }
        public async Task<Customer?> GetCustomerbyId(string customerid)

        {
            try

            {
                var customer = await connection.QueryFirstAsync<Customer>("select * from Customer where CustomerId= @id", new { id = customerid });

                if (customer != null)
                {
                    _logger.LogInformation("Customer details are loaded by admin Id successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Customer details are loaded by admin Id successfully");
                    return customer;
                }
                else
                {
                    _logger.LogWarning("No Customer details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Customer details are available");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured." + ex);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " " + "Error Occured." + ex);
                return null;
            }
            
          

        }
        public async Task<Customer?> CustomerLogin(string Email, string Password)
        {
            try
            {
                string encryptedpassword = Crypto.Encrypt(Password);
                var customer = await connection.QuerySingleOrDefaultAsync<Customer>("select * from Customer where Email= @email and Password=@password", new { email = Email, password = Crypto.Encrypt(Password) });

                if (customer != null)
                {
                    _logger.LogInformation("Customer Login to the system successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Customer Login to the system successfully" + "" + customer.Email + "" + customer.FirstName+"" + customer.LastName);
                    return customer;
                }
                else
                {
                    _logger.LogWarning("No Customer found");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Customer found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured." + ex);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " " + "Error Occured. " + ex);
                return null;
            }
           

        }
        public async Task<int?> RegisterCustomer(Customer customer)
        {
           
           
            try
            {

                Id id = factory.CreateId("Customer");
                customer.CustomerId = id.getId();
                customer.Password = Crypto.Encrypt(customer.Password);
                int result = await connection.ExecuteAsync("Insert into Customer (CustomerId, FirstName, LastName,DOB, MobileNo,Address, Email, Password, CreatedDate, StatusId) values(@CustomerId, @FirstName, @LastName,@DOB,@MobileNo,@Address, @Email, @Password, @CreatedDate, @StatusId)", customer);
                if (result > 0)
                {
                    _logger.LogInformation("customer Registered successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "customer Registered successfully" + "" +customer.Email + "" + customer.FirstName + "" + customer.LastName);
                    return result;
                }
                else
                {
                    _logger.LogWarning("Error Occured while saving customer");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Error Occured while saving customer");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured." + ex);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " " + "Error Occured. " + ex);
                return null;
            }

        }
        public async Task<string?> GetCustomerid()
        {
            
            try
            {
                Id id = factory.CreateId("Customer");
                string customerid = id.getId();
                
                if (customerid != null)
                {
                    _logger.LogInformation("Customer Id Created successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Customer Id Created successfully");
                    return customerid;
                }
                else
                {
                    _logger.LogWarning("Error Occured while Genarating Id");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Error Occured while Genarating Id");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured." + ex);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " " + "Error Occured. " + ex);
                return null;
            }

        }
        public async Task<bool> ExsistCustomer(string Email)
        {
            try
            {

                var customer = await connection.QuerySingleOrDefaultAsync<int>("SELECT COUNT(*) FROM customer WHERE Email = @email", new { email = Email });


                if (customer > 0)
                {
                    _logger.LogInformation("Customer Is Already Exist");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " Customer Is Already Exist" + " " + Email);
                    return true; // Login successful
                }
                else
                {
                    _logger.LogWarning("Customer Is Not Already Exist");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " Customer Is Not Already Exist");
                    return false; // Login failed
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occurred: " + ex);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " Error Occurred: " + ex);
                return false; // Login failed due to an exception
            }
        }
    }
}
