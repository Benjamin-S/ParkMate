using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using ParkMate.ApplicationServices.Interfaces;


namespace ParkMate.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private string _sendGridApiKey;

        public EmailService(IConfiguration configuration)
        {
            _sendGridApiKey = configuration["SendGrid:ServiceApiKey"];
        }

        public async Task SendEmailAsync(string emailAddress, string subject, string body)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var fromEmail = new EmailAddress("mail@parkmate.com", "ParkMate");
            var toEmail = new EmailAddress(emailAddress);

            var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, null, body);

            _ = await client.SendEmailAsync(msg);
        }
    }
}