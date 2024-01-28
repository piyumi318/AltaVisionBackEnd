using Dapper;
using Microsoft.AspNetCore.Mvc;
using SolarEcoBackEnd.Entity;
using System.Data.SqlClient;

namespace SolarEcoBackEnd.DB
{
    public class AdminDB
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();

        private IdFactory factory = new IdFactory();
         

        public async Task<IEnumerable<Admin>> GetAdmin()
        { 
            var getadmin = await connection.QueryAsync<Admin>("select * from Admin");

            return getadmin;

        }
        public async Task<Admin> GetAdminbyId(int adminid)
        { 
            var admin = await connection.QueryFirstAsync<Admin>("select * from Admin where AdminId= @id", new { id = adminid });
            return admin;

        }
        public async Task<int> RegisterAdmin(Admin admin)
        {
            Id id = factory.CreateId("Admin");
            admin.AdminId = id.getId();
            int result= await connection.ExecuteAsync("Insert into Admin (AdminId, AdminName, Email, Password, CreatedDate, StatusId) values(@AdminId, @AdminName, @Email, @Password, @CreatedDate, @StatusId)", admin);

            return result;

        }
        public async Task<string> GetAdminid()
        {
            Id id = factory.CreateId("Admin");
            string adminid = id.getId();
            return adminid;

        }

    }
}
