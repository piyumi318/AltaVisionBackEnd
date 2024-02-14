using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEnd.DataAcessLayer.Interfaces
{
    public interface IAppoinment
    {
        Task<IEnumerable<Appointment>?> GetAllAppoinment();
    }
}
