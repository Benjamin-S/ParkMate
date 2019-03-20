using System.Threading.Tasks;

namespace ParkMate.ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailAddress, string subject, string body);
    }
}