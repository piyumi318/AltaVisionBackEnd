namespace SolarEcoBackEnd.Entity
{
    public class Predictions
    {
        public string? PredictionId { get; set; }
        public int? WindSpeed { get; set; }
        public int? Radiation { get; set; }
        public int? AirTemperature { get; set; }
        public int? RelativeAirHumidity { get; set; }
        public int? Hour { get; set; }
        public int? Sunshine { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public DateTime? PredicteddDate { get; set; }
        
    }
}
