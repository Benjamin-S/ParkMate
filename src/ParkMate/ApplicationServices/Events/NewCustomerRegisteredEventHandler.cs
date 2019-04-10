using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class NewCustomerRegisteredEventHandler :
        INotificationHandler<NewCustomerRegisteredEvent>
    {
        private IDocumentWriteRepository _repository;

        public NewCustomerRegisteredEventHandler(IDocumentWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            NewCustomerRegisteredEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _repository.InsertOneAsync(notification.Customer, "Customer");
        }
    }
}
