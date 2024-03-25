using AltaVision.Logger;
using AltaVisionBackEnd.DataAcessLayer.DataAccess;
using Microsoft.Extensions.Logging;
using Moq;
using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEndTestProject
{
    public class PredictionTest
    {
        [Test]
        public async Task savePredictionSuccess()
        {

            var loggerMock = new Mock<ILogger<PredictionsDB>>();
            var logsMock = new Mock<ILogs>();

            var predictionsDB = new PredictionsDB(loggerMock.Object, logsMock.Object);


            var prediction = new Predictions
            {
                WindSpeed = (float?)1.38,
                Sunshine = (float?)3.47,
                Radiation = 10,
                AirTemperature = 23,
                RelativeAirHumidity = 90,
                Hour = 15,
                AirPressure = 1009,
                Month = 10,
                Day = 20,
                PredictedBy= "Customer0006"


            };

            int? result = await predictionsDB.SavePrediction(prediction);



            Assert.AreEqual(1, result);

        }

    }
}
