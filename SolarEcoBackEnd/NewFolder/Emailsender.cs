using System.Net;
using System.Net.Mail;

namespace SolarEcoBackEnd.NewFolder
{
    public class Emailsender:IEmailsender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "solarecolanka@gmail.com";
            var password = "solar@123";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail, to: email, subject, message));
        }
    }
}
