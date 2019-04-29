using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Interfaces;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class NewBookingSellerEmailHandler :
        INotificationHandler<NewBookingCreatedEvent>
    {
        private IEmailService _emailSender;

        public NewBookingSellerEmailHandler(IEmailService emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(
            NewBookingCreatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _emailSender.SendEmailAsync(notification.Seller.Email,
                "You have a new ParkMate booking",
                $"Dear {notification.Seller.Name},\n\n" +
                "You have a new ParkMate booking....");
        }
    }
}