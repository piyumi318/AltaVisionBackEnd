namespace SolarEcoBackEnd.Entity
{
    public class Appointment
    {
        public int AppoinmentId { get; set; }
        public string? Name { get; set; }
        public string? MoileNo { get; set; }
        public string? Address { get; set; }
        public DateTime? RequestedDate { get; set; }
    
    }
}
