using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEcoBackEnd.DataAcessLayer.Interfaces
{
 public interface IAdminDB
    {
        Task<IEnumerable<Admin>?> GetAdmin() ;
        Task<Admin?> GetAdminbyId(string adminid);
        Task<Admin?> AdminLogin(string Email, string Password);
        Task<int?> RegisterAdmin(Admin admin);
        Task<string?> GetAdminid();

    }
}
