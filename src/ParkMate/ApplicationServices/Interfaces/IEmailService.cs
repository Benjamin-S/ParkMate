using System.Threading.Tasks;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailAddress, string subject, string body);
    }
}
