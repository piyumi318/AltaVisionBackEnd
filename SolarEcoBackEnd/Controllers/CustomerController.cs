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
      //  CustomerDB DB = new CustomerDB();

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
               
                return Ok(getadmin);
            }
            else
            {
                return null;
              
            }
           
        }

        [HttpPost]
        [Route("RegisterCustomer")]
        public async Task<ActionResult<bool>> RegisterCustomer(Customer customer)
        {
            bool isSuccess = await _CustomerDB.RegisterCustomer(customer) > 0; // Assuming RegisterCustomer returns the number of affected rows
            return Ok(isSuccess);
        }
        [HttpGet]

        [Route("Exsist")]
        public async Task<bool> ExsistCustomer(string Email)
        {
            bool getadmin = await _CustomerDB.ExsistCustomer(Email);

            return getadmin;
        }

    }
}
