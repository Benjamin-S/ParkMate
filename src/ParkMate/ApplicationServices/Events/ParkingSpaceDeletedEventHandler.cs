using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Commands;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceDeletedEventHandler :
        INotificationHandler<ParkingSpaceDeletedEvent>
    {
        private IMediator _mediator;

        public ParkingSpaceDeletedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ParkingSpaceDeletedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new DeleteParkingSpaceFromDocumentDbCommand(notification.ParkingSpace);
            await _mediator.Send(command);
        }
    }
}
