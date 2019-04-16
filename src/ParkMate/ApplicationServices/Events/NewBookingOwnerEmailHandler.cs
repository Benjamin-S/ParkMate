using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Interfaces;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class NewBookingOwnerEmailHandler :
        INotificationHandler<NewBookingCreatedEvent>
    {
        private IEmailSender _emailSender;

        public NewBookingOwnerEmailHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(
            NewBookingCreatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            /*
            await _emailSender.SendEmailAsync(notification.ParkingSpace.Owner.Email,
                "You have a new ParkMate booking",
                $"Dear {notification.ParkingSpace.Owner.Name},\n\n" +
                "You have a new ParkMate booking....");
            */
        }
    }
}