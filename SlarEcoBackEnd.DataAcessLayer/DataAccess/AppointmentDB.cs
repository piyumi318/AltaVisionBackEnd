using AltaVision.Logger;
using AltaVisionBackEnd.DataAcessLayer.Interfaces;
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
   public class AppointmentDB:IAppointmentDB
    {
        static IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        static DBconnector db = DBconnector.GetInstance(configuration);
        SqlConnection connection = db.GetConnection();
        private readonly ILogger<AppointmentDB> _logger;
        private ILogs _logs;

        public AppointmentDB() { }
        public AppointmentDB( ILogger<AppointmentDB> logger, ILogs logs)
        {
           
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
        public async Task<Appointment?> GetAppoinmentByUser(string customerid)
        {

            try

            {
                var appointment = await connection.QueryFirstAsync<Appointment>("select * from Appointment where CustomerId= @id", new { id = customerid });

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

                DateTime date = DateTime.Now;
                int result = await connection.ExecuteAsync("Insert into Appointment (Name, MobileNo, Address, CustomerId,RequestedDate, IsReview) values(@Name, @MobileNo, @Address, @CustomerId,@RequestedDate, @IsReview)", appointment);
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
        public async Task<int?> ReviewAppointment(int Appointmentid)
        {
            try
            {

                DateTime date = DateTime.Now;
                int result = await connection.ExecuteAsync("Update Appointment Set IsReview= 1 where AppoinmentId=@id",new { id = Appointmentid });
                if (result > 0)
                {
                    _logger.LogInformation("Appointment Reviewed successfully");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Appointment Reviewed successfully");
                    return result;
                }
                else
                {
                    _logger.LogWarning("Error Occured while Review Appointment details");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " " + "Error Occured while Review Appointment details");
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
        public async Task<bool> ExsistAppointment(string Id)
        {
            try
            {

                var admin = await connection.QuerySingleOrDefaultAsync<int>("SELECT COUNT(*) FROM Appointment WHERE CustomerId = @id and IsReview=0", new { id = Id });


                if (admin > 0)
                {
                    _logger.LogInformation("Appointment Is Already Exist");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " Administor Is Already Exist" + " " + Id);
                    return true; // Login successful
                }
                else
                {
                    _logger.LogWarning("Appointment Is Not Already Exist");
                    Console.WriteLine();
                    _logs.WriteLog(DateTime.Now + " Appointment Is Not Already Exist");
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
