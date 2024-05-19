namespace SolarEcoBackEnd.Entity
{
    public class SolarPanel
    {
        public int SolarPanelId { get; set; }
        public string SolarPanelName { get; set; }
        public int Capacity { get; set; }
        public int MonthlyElectricityUsage { get; set; }
        public int? Price { get; set; }
        public string CreatedBy { get; set; }
        public int StatusId { get; set; }
        public int[] Capacities { get; set; }
        public int? Totalprice { get; set; }
    }
}
