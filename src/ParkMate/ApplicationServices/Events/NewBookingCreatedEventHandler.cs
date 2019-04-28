using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Commands;

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
            var command = new CreateBookingMaterializedViewCommand(notification.Booking);
            await _mediator.Send(command);
        }
    }
}