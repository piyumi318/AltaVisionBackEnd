namespace SolarEcoBackEnd.Entity
{
    public class SolarPanel
    {
        public int? SolarPanelId { get; set; }
        public int Capacity { get; set; }
        public int Price { get; set; }
        public int CreatedBy { get; set; }
        public int StatusId { get; set; }
    }
}
