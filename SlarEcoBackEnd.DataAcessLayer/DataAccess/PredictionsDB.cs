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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEnd.DataAcessLayer.DataAccess
{
  public  class PredictionsDB:IPredictionsDB
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();
        private readonly ILogger<PredictionsDB> _logger;
        private ILogs _logs;

        public PredictionsDB( ILogger<PredictionsDB> logger, ILogs logs)
        {
            
            _logger = logger;
            _logs = logs;
        }
        public async Task<IEnumerable<Predictions>?> GetAllPrediction()
        {


            try

            {
                var getPrediction = await connection.QueryAsync<Predictions>("select * from Prediction");
                if (getPrediction != null)
                {
                    _logger.LogInformation("Prediction details are loaded successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Prediction details are loaded successfully");
                    return getPrediction;
                }
                else
                {
                    _logger.LogWarning("No Prediction details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Prediction details are available");
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
        public async Task<Predictions?> GetPredictionbyuserId(string PredictedBy)
        {

            try

            {
                var prediction = await connection.QueryFirstAsync<Predictions>("select * from Prediction where PredictedBy= @id", new { id = PredictedBy });

                if (prediction != null)
                {
                    _logger.LogInformation("Prediction details are loaded by admin Id successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Prediction details are loaded by admin Id successfully");
                    return prediction;
                }
                else
                {
                    _logger.LogWarning("No Prediction details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Prediction details are available");
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
        public async Task<int?> SavePrediction(Predictions predictions)
        {
            try
            {


                int result = await connection.ExecuteAsync("SavePrediction", new
                {
                    WindSpeed=predictions.WindSpeed,
                    Sunshine=predictions.Sunshine,
                    Radiation = predictions.Radiation,
                    AirTemperature = predictions.AirTemperature,
                    RelativeAirHumidity = predictions.RelativeAirHumidity,
                    AirPressure = predictions.AirPressure,
                    Hour = predictions.Hour,
                    Month = predictions.Month,
                    Day = predictions.Day,
                    PredictedBy = predictions.PredictedBy
                }, commandType: System.Data.CommandType.StoredProcedure);
                if (result > 0)
                {
                    _logger.LogInformation("prediction detailsadded successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "prediction details added successfully");
                    return result;
                }
                else
                {
                    _logger.LogWarning("Error Occured while saving prediction details");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Error Occured while saving prediction details");
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
