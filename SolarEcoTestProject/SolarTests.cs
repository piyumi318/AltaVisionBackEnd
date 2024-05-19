using AltaVisionBackEnd.DataAcessLayer.Interfaces;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltaVisionBackEnd.DataAcessLayer.DataAccess;
using AltaVision.Logger;
using Microsoft.Extensions.Logging;
using Moq;
using SolarEcoBackEnd.DB;
using System.Data.SqlClient;
using Dapper;

namespace AltaVisionBackEndTestProject
{
    [TestFixture]
    public class SolarTests
    {
        [Test]
        public async Task CreateSolarPanel_Success()
        {

            var loggerMock = new Mock<ILogger<SolarPanelDB>>();
            var logsMock = new Mock<ILogs>();
            var solarPanelDB = new SolarPanelDB(loggerMock.Object, logsMock.Object);
            var solarPanel = new SolarPanel
            {
                Capacity = 8,
                SolarPanelName="Solar123",
                Price = 2900000,
                StatusId = 1,
                SolarPanelId = -1,
                CreatedBy = "Admin03"
            };
           
            int? result = await solarPanelDB.CreateSolarPanel(solarPanel);

            Assert.AreEqual(1, result);

        }
        [Test]
        public async Task UpdateSolarPanel_Success()
        {

            var loggerMock = new Mock<ILogger<SolarPanelDB>>();
            var logsMock = new Mock<ILogs>();
            var solarPanelDB = new SolarPanelDB(loggerMock.Object, logsMock.Object);
            var solarPanel = new SolarPanel
            {
               
                Price = 850000,
                StatusId = 2,
                SolarPanelId =5,
                CreatedBy = "Admin02"
            };
            int? result = await solarPanelDB.CreateSolarPanel(solarPanel);
            Assert.AreEqual(2, result);

        }

        [Test]
        public async Task InActiveSolarPanel_Success()
        {

            var loggerMock = new Mock<ILogger<SolarPanelDB>>();
            var logsMock = new Mock<ILogs>();
            // var dbMock = new Mock<DBconnector>();
            var solarPanelDB = new SolarPanelDB(loggerMock.Object, logsMock.Object);
           
            var solarPanel = new SolarPanel
            {
                Capacity = 5,
                Price = 750000,
                StatusId = 3,
                SolarPanelId = 3,
                CreatedBy = "Admin02"
            };
            int? result = await solarPanelDB.CreateSolarPanel(solarPanel);
            Assert.AreEqual(1, result);

        }

    }
}
