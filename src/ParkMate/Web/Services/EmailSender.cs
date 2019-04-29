using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private IEmailService _emailService;

        public EmailSender(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendEmailAsync(string emailAddress, string subject, string body)
        {
            await _emailService.SendEmailAsync(emailAddress, subject, body);
        }
    }
}