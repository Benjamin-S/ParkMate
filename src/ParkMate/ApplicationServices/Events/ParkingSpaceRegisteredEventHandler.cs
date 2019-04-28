using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceRegisteredEventHandler : 
        INotificationHandler<ParkingSpaceRegisteredEvent>
    {
        private IMediator _mediator;

        public ParkingSpaceRegisteredEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(
            ParkingSpaceRegisteredEvent notification, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new AddParkingSpaceMaterializedViewCommand(notification.ParkingSpace);
            await _mediator.Send(command);
        }
    }
}
