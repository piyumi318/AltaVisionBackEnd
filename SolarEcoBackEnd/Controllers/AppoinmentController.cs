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
    public class AppoinmentController : ControllerBase
    {
        private readonly ILogger<AppoinmentController> _logger;
        private readonly AppointmentDB _appoinmentDB;
        public AppoinmentController(ILogger<AppoinmentController> logger, AppointmentDB appoinmentDB) 
        {
            _logger = logger;
            _appoinmentDB = appoinmentDB;
        }

        [HttpPost]

        [Route("MakeAppointment")]
        public async Task<ActionResult<List<Appointment>>> MakeAppointment(Appointment appointment)
        {
            await _appoinmentDB.MakeAppointment(appointment);
            return Ok();
        }

        [HttpGet]

        [Route("GetAllAppointment")]
        public async Task<ActionResult<List<Appointment>>> GetAllAppoinment()
        {
            var getadmin = await _appoinmentDB.GetAllAppoinment();


            return Ok(getadmin);

        }
        [HttpGet]

        [Route("GetAppoinmentByuserId")]
        public async Task<ActionResult<Admin>> GetAppoinmentByUser(string userid)
        {
            var getadmin = await _appoinmentDB.GetAppoinmentByUser(userid);

            return Ok(getadmin);
        }
    }
}
