using Cryptography;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SolarEcoBackEnd.DataAcessLayer.Interfaces;
using SolarEcoBackEnd.Entity;
using System.Data.SqlClient;
using AltaVision.Logger;

namespace SolarEcoBackEnd.DB
{
    public class AdminDB:IAdminDB
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();
        private readonly ILogger<AdminDB> _logger;
        private IdFactory factory = new IdFactory();
        private ILogs _logs;
        
        public AdminDB() 
        {

        }
        public AdminDB(ILogger<AdminDB> logger, ILogs logs)
        {
            _logger = logger;
            _logs = logs;
        }

        public async Task<IEnumerable<Admin>?> GetAdmin()
        { 
           

            try

            {
                var getadmin = await connection.QueryAsync<Admin>("select * from Admin");
                if (getadmin != null)
                {
                  _logger.LogInformation("Administors details are loaded successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now+" "+"Administors details are loaded successfully");
                    return getadmin;
                }
                else
                {
                    _logger.LogWarning("No Administors details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Administors details are available");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured."+ex);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " " + "Error Occured." + ex);
                return null;
            }
            

        }
        public async Task<Admin?> GetAdminbyId(string adminid)
        { 
            
            try

            {
                var admin = await connection.QueryFirstAsync<Admin>("select * from Admin where AdminId= @id", new { id = adminid });
                
                if (admin != null)
                {
                    _logger.LogInformation("Administors details are loaded by admin Id successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Administors details are loaded by admin Id successfully");
                    return admin;
                }
                else
                {
                    _logger.LogWarning("No Administors details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Administors details are available");
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
        public async Task<Admin?> AdminLogin(string Email, string Password)

        {   try
            {
                string encryptedpassword = Crypto.Encrypt(Password);
                var admin = await connection.QuerySingleOrDefaultAsync<Admin>("select * from Admin where Email= @email and Password=@password", new { email = Email, password = encryptedpassword });

                if (admin != null)
                {
                    _logger.LogInformation("Administor Login to the system successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Administor Login to the system successfully"+"" +admin.AdminName+""+admin.Email);
                    return admin;
                }
                else
                {
                    _logger.LogWarning("No Administors found");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Administors found");
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
        public async Task<int ?> RegisterAdmin(Admin admin)
        {
            try
            {
                
                    Id id = factory.CreateId("Admin");
                    admin.AdminId = id.getId();
                    admin.Password = Crypto.Encrypt(admin.Password);
                    int result = await connection.ExecuteAsync("Insert into Admin (AdminId, AdminName, Email, Password, CreatedDate, StatusId) values(@AdminId, @AdminName, @Email, @Password, @CreatedDate, @StatusId)", admin);
                if (result>0)
                {
                    _logger.LogInformation("Administor Registered successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Administor Registered successfully" + "" + admin.AdminName + "" + admin.Email);
                    return result;
                }
                else
                {
                    _logger.LogWarning("Error Occured while saving Administor");
                    Console.WriteLine();
                   _logs.WriteLog(DateTime.Now + " " + "Error Occured while saving Administor");
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
        public async Task<string?> GetAdminid()
        {
            try
            {
                Id id = factory.CreateId("Admin");
                string adminid = id.getId();
                if (adminid!=null) 
                {
                   _logger.LogInformation("Administor Id Created successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Administor Id Created successfully");
                   return adminid;
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
       

    }
}
