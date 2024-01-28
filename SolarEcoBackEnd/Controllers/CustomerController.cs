using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarEcoBackEnd.DataAcessLayer.Interfaces;
using SolarEcoBackEnd.DB;
using SolarEcoBackEnd.Entity;

namespace SolarEcoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerDB _CustomerDB;
        CustomerDB DB = new CustomerDB();

        public CustomerController(ICustomerDB customerDB)
        {
            _CustomerDB = customerDB;
        }

        [HttpGet]

        [Route("GetAllCustomer")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomer()
        {
            var getadmin = await _CustomerDB.GetCustomer();
             return Ok(getadmin);

        }
        [HttpGet]

        [Route("GetCustomerId")]
        public async Task<string> GetCustomerId()
        {
            string getadmin = await _CustomerDB.GetCustomerid();


            return getadmin;

        }

        [HttpGet]

        [Route("GetCustomerbyId")]
        public async Task<ActionResult<Customer>> GetAdminbyId(string customerid)
        {
            var getadmin = await _CustomerDB.GetCustomerbyId(customerid);
             return Ok(getadmin);
        }
        [HttpGet]

        [Route("customerLogin")]
        public async Task<ActionResult<Customer>?> GetAdminLogin(string email, string password)
        {
            var getadmin = await _CustomerDB.CustomerLogin(email, password);
            if (getadmin != null)
            {
                // Process the result
                return Ok(getadmin);
            }
            else
            {
                return null;
                // Handle the case where no elements are found
            }
           
        }

        [HttpPost]

        [Route("RegisterCustomer")]
        public async Task<ActionResult<List<Customer>>> RegisterCustomer(Customer customer)
        {
           await _CustomerDB.RegisterCustomer(customer);
            return Ok();
        }

    }
}
