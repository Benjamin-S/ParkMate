using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Interfaces;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class NewBookingCustomerEmailHandler :
        INotificationHandler<NewBookingCreatedEvent>
    {
        private IEmailSender _emailSender;

        public NewBookingCustomerEmailHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(
            NewBookingCreatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _emailSender.SendEmailAsync(notification.Customer.Email,
                "Your ParkMate Booking",
                $"Dear {notification.Customer.Name},\n\n" +
                "You have succesfully booked....");
        }
    }
}