using Dapper;
using SolarEcoBackEnd.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace SolarEcoBackEnd.DB
{
    public class AdminId : Id
    {
       
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();
        private static readonly ILogger logger;
        private  string _adminId;
        public AdminId()
        {
            this._adminId = _adminId;
        }
        public async Task<string> maxId()
        {
            try
            {
                var maxId = await connection.QueryFirstAsync<string>("select max(AdminId) from admin");
                return _adminId=maxId;
            }
            catch (Exception ex)
            {
                 ex.ToString(); logger.LogError("This is an error message." + ex);
                return _adminId = null;
                    //ex.ToString();
            }
           
        }
        public String getId() 
        { 
        
        string adminId = null;
            try
            {Task.Run(() => maxId()).Wait();

                string Id = _adminId;

                if (Id != null)
                {

                    string idString = Id.Substring(5);
                    int CTR = Int32.Parse(idString);
                    if (CTR >= 1 && CTR <= 9)
                    {
                        CTR = CTR + 1;
                        adminId = "Admin0" + CTR;
                    }
                    else if (CTR >= 10 && CTR <=99)
                    {
                        CTR = CTR + 1;
                        adminId = "Admin" + CTR;
                    }
                    
                }

                else { adminId = "Admin01"; }
            }

            catch (Exception ex) { adminId = ex.ToString(); logger.LogError("This is an error message."+ ex); }


                return adminId;
            }
        }
    
}
