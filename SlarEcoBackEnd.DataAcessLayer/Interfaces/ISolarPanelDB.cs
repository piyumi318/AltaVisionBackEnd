using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEnd.DataAcessLayer.Interfaces
{
    public interface ISolarPanelDB
    {
        Task<int?> CreateSolarPanel(SolarPanel solarPanel);
        Task<IEnumerable<SolarPanel>?> GetAllSolarPanels();
        Task<int> CalculateCostOfSolarPanel(int[] units);
        int[] CalculateRequiredCapacity(int unit);
    }
}
