using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Commands;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceUpdatedEventHandler :
        INotificationHandler<ParkingSpaceUpdatedEvent>
    {
        private IMediator _mediator;

        public ParkingSpaceUpdatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ParkingSpaceUpdatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new ReplaceParkingSpaceMaterializedViewCommand(notification.ParkingSpace);
            await _mediator.Send(command);
        }
    }
}
