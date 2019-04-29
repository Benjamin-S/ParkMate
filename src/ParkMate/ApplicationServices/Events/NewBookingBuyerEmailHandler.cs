using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Interfaces;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class NewBookingBuyerEmailHandler :
        INotificationHandler<NewBookingCreatedEvent>
    {
        private IEmailService _emailSender;

        public NewBookingBuyerEmailHandler(IEmailService emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(
            NewBookingCreatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _emailSender.SendEmailAsync(notification.Buyer.Email,
                "Your ParkMate Booking",
                $"Dear {notification.Buyer.Name},\n\n" +
                "You have succesfully booked....");
        }
    }
}