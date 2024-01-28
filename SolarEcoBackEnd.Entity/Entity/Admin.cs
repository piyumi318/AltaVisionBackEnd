namespace SolarEcoBackEnd.Entity
{
    public class Admin
    {
        public string? AdminId { get; set; }
        public string? AdminName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int StatusId { get; set; }
    }
   
}
