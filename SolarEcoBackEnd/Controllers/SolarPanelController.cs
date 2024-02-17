using AltaVisionBackEnd.DataAcessLayer.DataAccess;
using AltaVisionBackEnd.DataAcessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarEcoBackEnd.Entity;

namespace AltaVisionBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolarPanelController : ControllerBase
    {
        private readonly ILogger<SolarPanelController> _logger;
        private readonly ISolarPanelDB _solarPanelDB;
        public SolarPanelController(ILogger<SolarPanelController> logger, ISolarPanelDB solarPanelDB)
        {
            _logger = logger;
            _solarPanelDB = solarPanelDB;
        }
        [HttpPost]

        [Route("CreateSolarPanel")]
        public async Task<ActionResult<List<SolarPanel>>> CreateSolarPanel(SolarPanel solarPanel)
        {
            await _solarPanelDB.CreateSolarPanel(solarPanel);
            return Ok();
        }

        [HttpGet]

        [Route("GetAllSolarpanels")]
        public async Task<ActionResult<List<Appointment>>> GetAllSolarPanels()
        {
            var getSlolarPanels = await _solarPanelDB.GetAllSolarPanels();


            return Ok(getSlolarPanels);

        }

    }
}
