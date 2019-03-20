using System.Threading.Tasks;
using ParkMate.ApplicationCore.Interfaces;

namespace ParkMate.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string emailAddress, string subject, string body)
        {
            throw new System.NotImplementedException();
        }
    }
}