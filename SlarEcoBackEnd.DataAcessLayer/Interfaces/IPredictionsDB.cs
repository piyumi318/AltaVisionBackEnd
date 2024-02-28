using SolarEcoBackEnd.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaVisionBackEnd.DataAcessLayer.Interfaces
{
    public interface IPredictionsDB
    {
        Task<int?> SavePrediction(Predictions predictions);
        Task<Predictions?> GetPredictionbyuserId(string PredictedBy);
        Task<IEnumerable<Predictions>?> GetAllPrediction();
    }
}
