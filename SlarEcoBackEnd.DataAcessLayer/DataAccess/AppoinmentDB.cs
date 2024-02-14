using AltaVision.Logger;
using Cryptography;
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
   public class AppoinmentDB
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();
        private readonly ILogger<AppoinmentDB> _logger;
        private ILogs _logs;

        public AppoinmentDB(SqlConnection connection, ILogger<AppoinmentDB> logger, ILogs logs)
        {
            this.connection = connection;
            _logger = logger;
            _logs = logs;
        }
        public async Task<IEnumerable<Appointment>?> GetAllAppoinment()
        {


            try

            {
                var getAppointment = await connection.QueryAsync<Appointment>("select * from Appointment");
                if (getAppointment != null)
                {
                    _logger.LogInformation("Appointment details are loaded successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Appointment details are loaded successfully");
                    return getAppointment;
                }
                else
                {
                    _logger.LogWarning("No Appointment details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Appointment details are available");
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
        public async Task<Appointment?> GetAppoinmentByUser(string userid)
        {

            try

            {
                var appointment = await connection.QueryFirstAsync<Appointment>("select * from Admin where AdminId= @id", new { id = userid });

                if (appointment != null)
                {
                    _logger.LogInformation("Appointment details are loaded by user Id successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Appointment details are loaded by user Id successfully");
                    return appointment;
                }
                else
                {
                    _logger.LogWarning("No Appointment details are available");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "No Appointment details are available");
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
        public async Task<int?> MakeAppointment(Appointment appointment)
        {
            try
            {

                
                int result = await connection.ExecuteAsync("Insert into Appointment (AdminId, AdminName, Email, Password, CreatedDate, StatusId) values(@AdminId, @AdminName, @Email, @Password, @CreatedDate, @StatusId)", appointment);
                if (result > 0)
                {
                    _logger.LogInformation("Appointment make successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Appointment make successfully");
                    return result;
                }
                else
                {
                    _logger.LogWarning("Error Occured while saving Appointment details");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Error Occured while saving Appointment details");
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
