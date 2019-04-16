using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Interfaces;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class NewBookingCreatedEventHandler :
        INotificationHandler<NewBookingCreatedEvent>
    {
        private IMediator _mediator;

        public NewBookingCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(
            NewBookingCreatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.Publish(new CustomerUpdatedEvent(notification.Customer));
            await _mediator.Publish(new ParkingSpaceUpdatedEvent(notification.ParkingSpace));
        }
    }
}