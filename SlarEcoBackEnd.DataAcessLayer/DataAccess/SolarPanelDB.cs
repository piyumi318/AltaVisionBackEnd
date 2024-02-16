using AltaVision.Logger;
using AltaVisionBackEnd.DataAcessLayer.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SolarEcoBackEnd.DB;
using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEnd.DataAcessLayer.DataAccess
{
    public class SolarPanelDB:ISolarPanelDB
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
        public async Task<int?> CreateSolarPanel(SolarPanel solarPanel)
        {
            try
            {


                int result = await connection.ExecuteAsync("SaveSolarPanel", new
                {
                    Capacity = solarPanel.Capacity,
                    Price = solarPanel.Price,
                    StatusId = solarPanel.StatusId,
                    SolarPanelId = solarPanel.SolarPanelId,
                    CreatedBy = solarPanel.CreatedBy
                }, commandType: System.Data.CommandType.StoredProcedure);
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
        public async Task<IEnumerable<SolarPanel>?> GetAllSolarPanels()
        {


            try

            {
                var getSolarPanels = await connection.QueryAsync<SolarPanel>("select * from solarPanel where isactive=1");
                if (getSolarPanels != null)
                {
                    _logger.LogInformation("Solar panel details are loaded successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Solar panel details are loaded successfully");
                    return getSolarPanels;
                }
                else
                {
                    _logger.LogWarning("No Solar panel details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Solar panel details are available");
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
    }
}
