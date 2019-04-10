using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class CustomerUpdatedEventHandler :
        INotificationHandler<CustomerUpdatedEvent>
    {
        private IDocumentWriteRepository _repository;

        public CustomerUpdatedEventHandler(IDocumentWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            CustomerUpdatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _repository.ReplaceOneAsync(notification.Customer, "Customer");
        }
    }
}
