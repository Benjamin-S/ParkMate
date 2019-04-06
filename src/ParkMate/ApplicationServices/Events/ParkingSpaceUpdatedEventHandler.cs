using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceUpdatedEventHandler :
        INotificationHandler<ParkingSpaceUpdatedEvent>
    {
        private IDocumentWriteRepository _repository;

        public ParkingSpaceUpdatedEventHandler(IDocumentWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            ParkingSpaceUpdatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _repository.ReplaceOneAsync(notification.ParkingSpace, "ParkingSpace");
        }
    }
}
