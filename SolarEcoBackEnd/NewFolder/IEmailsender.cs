namespace SolarEcoBackEnd.NewFolder
{
    public interface IEmailsender
    {
        public  Task SendEmailAsync(string email, string subject, string message);
        
    }
}
