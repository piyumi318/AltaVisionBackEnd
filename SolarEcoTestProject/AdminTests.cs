using AltaVision.Logger;
using Microsoft.Extensions.Logging;
using Moq;
using SolarEcoBackEnd.DB;
using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEndTestProject
{
    public class AdminTests

    {
        [Test]
        public async Task RegisterAdmin_Success()
        {

            var loggerMock = new Mock<ILogger<AdminDB>>();
            var logsMock = new Mock<ILogs>();

            var adminDB = new AdminDB(loggerMock.Object, logsMock.Object);

            var admin = new Admin
            {
                AdminName = "Piyumi",
                Email = "admin@gmail.com",
                Password = "admin@318",
                CreatedDate = DateTime.Now,
                StatusId = 1
            };

            int? result = await adminDB.RegisterAdmin(admin);



            Assert.AreEqual(1, result);

        }
    }
}
