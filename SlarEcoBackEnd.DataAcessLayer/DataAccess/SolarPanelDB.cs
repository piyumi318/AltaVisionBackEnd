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

        public SolarPanelDB()
        {
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
        public int[] CalculateRequiredCapacity(int unit)
        {
            List<int> requiredCapacity = new List<int>();

            // Deduct 600 if unit is greater than 600
            if (unit > 600)
            {
                unit -= 600;
                requiredCapacity.Add(5);
            }

            // Determine rate based on the remaining unit value
            switch (unit)
            {
                case int n when (n >= 1 && n < 240):
                    requiredCapacity.Add(2);
                    break;
                case int n when (n >= 240 && n < 360):
                    requiredCapacity.Add(3);
                    break;
                case int n when (n >= 360 && n < 480):
                    requiredCapacity.Add(4);
                    break;
                case int n when (n >= 480 && n < 600):
                    requiredCapacity.Add(5);
                    break;
                default:
                    // For any remaining cases, return 6
                    requiredCapacity.Add(0);
                    break;
            }

            // Convert the list to an array and return
            return requiredCapacity.ToArray();
        }
        public async Task<int> CalculateCostOfSolarPanel(int[] Capacity)
        {
            int totalPrice = 0;

            try
            {
                foreach (int capacity in Capacity)
                {
                    string query = "SELECT price FROM solarPanel WHERE capacity = @Capacity AND isActive = 1";
                    var price = await connection.QueryFirstOrDefaultAsync<int>(query, new { Capacity = capacity });

                    if (price > 0) 
                    {
                        totalPrice += price;
                    }
                }

                _logger.LogInformation("Total price calculated successfully: " + totalPrice);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " Total price calculated successfully: " + totalPrice);

                return totalPrice;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while calculating total price: " + ex.Message);
                Console.WriteLine();
                _logs.WriteLog(DateTime.Now + " Error occurred while calculating total price: " + ex.Message);

                throw; 
            }
        }


    
}
}
