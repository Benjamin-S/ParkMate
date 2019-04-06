using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceRegisteredEventHandler : 
        INotificationHandler<ParkingSpaceRegisteredEvent>
    {
        private IDocumentWriteRepository _repository;

        public ParkingSpaceRegisteredEventHandler(IDocumentWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            ParkingSpaceRegisteredEvent notification, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _repository.InsertOneAsync(notification.ParkingSpace, "ParkingSpace");
        }
    }
}
