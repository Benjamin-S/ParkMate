using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class CustomerUpdatedEventHandler :
        INotificationHandler<CustomerUpdatedEvent>
    {
        private IMediator _mediator;

        public CustomerUpdatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(
            CustomerUpdatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new ReplaceCustomerMaterializedViewCommand(notification.Customer);
            await _mediator.Send(command);
        }
    }
}
