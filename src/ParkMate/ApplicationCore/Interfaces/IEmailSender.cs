namespace ParkMate.ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string emailAddress, string subject, string body);
    }
}