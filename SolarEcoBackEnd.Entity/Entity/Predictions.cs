namespace SolarEcoBackEnd.Entity
{
    public class Predictions
    {
        public int? PredictionId { get; set; }
        public float? WindSpeed { get; set; }
        public float? Radiation { get; set; }
        public float? AirTemperature { get; set; }
        public float? RelativeAirHumidity { get; set; }
        public int? Hour { get; set; }
        public float? Sunshine { get; set; }
        public int? Month { get; set; }
        public int? AirPressure { get; set; }
        public int? Day { get; set; }
        public decimal? SolarPowerProduction { get; set; }
        public string? PredictedBy { get; set; }
        public DateTime? PredictedDate { get; set; }
        
    }
}
