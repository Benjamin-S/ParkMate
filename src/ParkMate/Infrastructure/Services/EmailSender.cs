using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ParkMate.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private string _sendGridApiKey = null;

        public EmailSender(IConfiguration configuration)
        {
            _sendGridApiKey = configuration["SendGrid:ServiceApiKey"];
        }

        public async Task SendEmailAsync(string emailAddress, string subject, string body)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var fromEmail = new EmailAddress("mail@parkmate.com");
            var toEmail = new EmailAddress(emailAddress);

            var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, null, body);

            var response = await client.SendEmailAsync(msg);
        }
    }
}