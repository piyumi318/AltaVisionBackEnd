using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEcoBackEnd.DataAcessLayer.Interfaces
{
    public interface ICustomerDB
    {
        Task<IEnumerable<Customer>?> GetCustomer();
        Task<Customer?> GetCustomerbyId(string customerid);
        Task<Customer?> CustomerLogin(string Email, string Password);


        Task<int?> RegisterCustomer(Customer customer);
        Task<string ?> GetCustomerid();
        Task<bool> ExsistCustomer(string Email);
    }
}
