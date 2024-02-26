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
            // var dbMock = new Mock<DBconnector>();
            var solarPanelDB = new SolarPanelDB(loggerMock.Object, logsMock.Object);


            // Arrange
            var solarPanel = new SolarPanel
            {
                Capacity = 2,
                Price = 2900000,
                StatusId = 1,
                SolarPanelId = -1,
                CreatedBy = 5
            };
            //   AppoinmentDB db = new AppoinmentDB(_logger, _logs);
            int? result = await solarPanelDB.CreateSolarPanel(solarPanel);



            Assert.AreEqual(1, result);

        }


    }
}
