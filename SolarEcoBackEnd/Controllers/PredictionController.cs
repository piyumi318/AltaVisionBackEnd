using AltaVisionBackEnd.DataAcessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarEcoBackEnd.Controllers;
using SolarEcoBackEnd.DataAcessLayer.Interfaces;
using SolarEcoBackEnd.DB;
using SolarEcoBackEnd.Entity;

namespace AltaVisionBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly ILogger<PredictionController> _logger;
        private readonly IPredictionsDB _predictionsDB;

        public PredictionController(ILogger<PredictionController> logger, IPredictionsDB predictionsDB)
        {
            _logger = logger;
            _predictionsDB = predictionsDB;
        }
        [HttpGet]

        [Route("GetPrediction")]
        public async Task<ActionResult<List<Admin>>> GetAdmin()
        {
            var getprediction = await _predictionsDB.GetAllPrediction();


            return Ok(getprediction);

        }
        [HttpGet]

        [Route("GetPredictionsbyUserId")]
        public async Task<ActionResult<Predictions>> GetAdminbyId(string userid)
        {
            var getPrediction = await _predictionsDB.GetPredictionbyuserId(userid);

            return Ok(getPrediction);
        }

        [HttpPost]

        [Route("SavePrediction")]
        public async Task<ActionResult<List<Predictions>>> SavePredictions(Predictions predictions)
        {
            await _predictionsDB.SavePrediction(predictions);
            return Ok();
        }


    }
}
