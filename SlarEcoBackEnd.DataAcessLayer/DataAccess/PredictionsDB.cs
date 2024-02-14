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
  public  class PredictionsDB
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
        //public async Task<IEnumerable<Admin>?> GetAdmin()
        //{


        //    try

        //    {
        //        var getadmin = await connection.QueryAsync<Admin>("select * from Prediction");
        //        if (getadmin != null)
        //        {
        //            _logger.LogInformation("Administors details are loaded successfully");
        //            Console.WriteLine();
        //            _logs.WriteLog(DateTime.Now + " " + "Administors details are loaded successfully");
        //            return getadmin;
        //        }
        //        else
        //        {
        //            _logger.LogWarning("No Administors details are available");
        //            Console.WriteLine();
        //            _logs.WriteLog(DateTime.Now + " " + "No Administors details are available");
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error Occured." + ex);
        //        Console.WriteLine();
        //        _logs.WriteLog(DateTime.Now + " " + "Error Occured." + ex);
        //        return null;
        //    }


        //}
    }
}
