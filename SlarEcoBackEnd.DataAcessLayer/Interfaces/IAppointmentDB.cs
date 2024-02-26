using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEnd.DataAcessLayer.Interfaces
{
    public interface AppointmentDB
    {
        Task<IEnumerable<Appointment>?> GetAllAppoinment();
        Task<int?> MakeAppointment(Appointment appointment);
        Task<Appointment?> GetAppoinmentByUser(string userid);
    }
}
