using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Commands;

namespace ParkMate.ApplicationServices.Events
{
    public class NewCustomerRegisteredEventHandler :
        INotificationHandler<NewCustomerRegisteredEvent>
    {
        private IMediator _mediator;

        public NewCustomerRegisteredEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(
            NewCustomerRegisteredEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new AddCustomerMaterializedViewCommand(notification.Customer);
            await _mediator.Send(command);
        }
    }
}
