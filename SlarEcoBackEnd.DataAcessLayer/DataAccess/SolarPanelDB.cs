using AltaVision.Logger;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SolarEcoBackEnd.DB;
using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEnd.DataAcessLayer.DataAccess
{
    public class SolarPanelDB
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();
        private readonly ILogger<SolarPanelDB> _logger;
        private ILogs _logs;

        public SolarPanelDB(ILogger<SolarPanelDB> logger, ILogs logs)
        {

            _logger = logger;
            _logs = logs;
        }
        public async Task<int?> SaveSolarPanel(SolarPanel solarPanel)
        {
            try
            {


                int result = await connection.ExecuteAsync("Insert into Appointment (Name, MobileNo, Email, Password, CreatedDate, StatusId) values(@Name, @AdminName, @Email, @Password, @CreatedDate, @StatusId)", solarPanel);
                if (result > 0)
                {
                    _logger.LogInformation("Solar panel make successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Solar panel make successfully");
                    return result;
                }
                else
                {
                    _logger.LogWarning("Error Occured while saving Solar panel details");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Error Occured while saving Solar panel details");
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
