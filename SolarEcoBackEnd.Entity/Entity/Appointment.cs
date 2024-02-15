namespace SolarEcoBackEnd.Entity
{
    public class Appointment
    {
        public int AppoinmentId { get; set; }
        public string? Name { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public string? CustomerId { get; set; }
        public DateTime? RequestedDate { get; set; }
    
    }
}
