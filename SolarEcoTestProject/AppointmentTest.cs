using AltaVision.Logger;
using AltaVisionBackEnd.DataAcessLayer.DataAccess;
using AltaVisionBackEnd.DataAcessLayer.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using Moq;
using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEndTestProject

{
    using NUnit.Framework;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Dapper;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Logging;
    using SolarEcoBackEnd.DB;

    [TestFixture]
    public class AppointmentDBTests
    {
        
          [Test]
        public async Task MakeAppointment_Success()
        {

            var loggerMock = new Mock<ILogger<AppoinmentDB>>();
            var logsMock = new Mock<ILogs>();
           // var dbMock = new Mock<DBconnector>();
            var appointmentDB = new AppoinmentDB(loggerMock.Object, logsMock.Object);


            // Arrange
            var appointment = new Appointment
            {
                Name = "John Doe",
                MobileNo = "1234567890",
                Address = "123 Main St",
                CustomerId = "cooo1"
            };
         //   AppoinmentDB db = new AppoinmentDB(_logger, _logs);
          int? result= await appointmentDB.MakeAppointment(appointment);

           
           
            Assert.AreEqual(1, result);

        }
           

    }
}
