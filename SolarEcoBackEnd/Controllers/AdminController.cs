using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarEcoBackEnd.DataAcessLayer;
using System.Data.SqlClient;
using SolarEcoBackEnd.Entity;
using SolarEcoBackEnd.DB;
using SolarEcoBackEnd.DataAcessLayer.Interfaces;

namespace SolarEcoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase

    {
        
        AdminDB DB = new AdminDB();
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminDB _adminDB;

        public AdminController(ILogger<AdminController> logger,IAdminDB adminDB)
        {
            _logger = logger;
            _adminDB = adminDB;
        }

        [HttpGet]
      
        [Route("GetAdmin")]
         public async Task<ActionResult<List<Admin>>> GetAdmin()
        {
            var getadmin = await _adminDB.GetAdmin();
          

                return Ok(getadmin);

        }

        [HttpGet]

        [Route("GetAdminId")]
        public async Task<string> GetAdminId()
        {
            string getadmin = await _adminDB.GetAdminid();
           
                 return getadmin;

        }

        [HttpGet]

        [Route("GetAdminbyId")]
        public async Task<ActionResult<Admin>> GetAdminbyId(string adminid)
        {
            var getadmin= await _adminDB.GetAdminbyId(adminid);
      
            return Ok(getadmin);
        }

        [HttpGet]

        [Route("AdminLogin")]
        public async Task<ActionResult<Admin>> GetAdminLogin(string email, string password)
        {
            var getadmin = await _adminDB.AdminLogin(email,password);

            return Ok(getadmin);
        }

        [HttpPost]

        [Route("RegisterAdmin")]
        public async Task<ActionResult<List<Admin>>> RegisterAdmin(Admin admin)
        {
            await _adminDB.RegisterAdmin(admin);
            return Ok();
        }


    }
}
