using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarEcoBackEnd.NewFolder;

namespace SolarEcoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class emailController : ControllerBase
    {
        private readonly IEmailsender _emailsender;
        public emailController(IEmailsender emailsender)
        {
            _emailsender = emailsender;
        }
        [HttpGet]

        [Route("sendemail")]
        public async Task<OkResult> GetAdmin()
        {
            await _emailsender.SendEmailAsync("piyumirajapaksha318@gmail.com", "Greet", "hello");
            // var getadmin = await connection.QueryAsync<Admin>("select * from Admin");

            return Ok();

        }
    }
}
