using AltaVision.Logger;
using AltaVisionBackEnd.DataAcessLayer.DataAccess;
using AltaVisionBackEnd.DataAcessLayer.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using Moq;

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
    using SolarEcoBackEnd.Entity;

    [TestFixture]
    public class AppointmentDBTests
    {
        
          [Test]
        public async Task MakeAppointment_Success()
        {

            var loggerMock = new Mock<ILogger<AppointmentDB>>();
            var logsMock = new Mock<ILogs>();
        
            var appointmentDB = new AppointmentDB(loggerMock.Object, logsMock.Object);


            var appointment = new Appointment
            {
                Name = "John Doe",
                MobileNo = "1234567890",
                Address = "123 Main St",
                CustomerId = "Customer001"
            };
       
          int? result= await appointmentDB.MakeAppointment(appointment);

           
           
            Assert.AreEqual(1, result);

        }
           

    }
}
