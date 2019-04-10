using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class ParkingSpaceDeletedEventHandler :
        INotificationHandler<ParkingSpaceDeletedEvent>
    {
        private IDocumentWriteRepository _repository;

        public ParkingSpaceDeletedEventHandler(IDocumentWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            ParkingSpaceDeletedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _repository.DeleteOneAsync(notification.ParkingSpace, "ParkingSpace");
        }
    }
}
