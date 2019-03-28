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
        private ISendGridClient _emailClient;

        public EmailSender(IConfiguration configuration, ISendGridClient emailClient)
        {
            _sendGridApiKey = configuration["SendGrid:ServiceApiKey"];
            _emailClient = emailClient;
        }

        public async Task SendEmailAsync(string emailAddress, string subject, string body)
        {

            var fromEmail = new EmailAddress("mail@parkmate.com");
            var toEmail = new EmailAddress(emailAddress);

            var msg = MailHelper.CreateSingleEmail(fromEmail, toEmail, subject, null, body);

            var response = await _emailClient.SendEmailAsync(msg);
        }
    }
}