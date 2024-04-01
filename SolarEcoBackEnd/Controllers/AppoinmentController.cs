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
        private readonly IAppointmentDB _appoinmentDB;
        public AppoinmentController(ILogger<AppoinmentController> logger, IAppointmentDB appoinmentDB) 
        {
            _logger = logger;
            _appoinmentDB = appoinmentDB;
        }

        [HttpPost]

        [Route("MakeAppointment")]
        public async Task<ActionResult<bool>> MakeAppointment(Appointment appointment)
        {
            bool isSuccess= await _appoinmentDB.MakeAppointment(appointment)>0;
            return Ok(isSuccess);
        }
        [HttpPut]

        [Route("ReviewAppointment")]
        public async Task<ActionResult<bool>> ReviewAppointment(int appointmentid)
        {
            bool isSuccess = await _appoinmentDB.ReviewAppointment(appointmentid) > 0;
            return Ok(isSuccess);
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
        public async Task<ActionResult<Appointment>> GetAppoinmentByUser(string userid)
        {
            var getadmin = await _appoinmentDB.GetAppoinmentByUser(userid);

            return Ok(getadmin);
        }
    }
}
