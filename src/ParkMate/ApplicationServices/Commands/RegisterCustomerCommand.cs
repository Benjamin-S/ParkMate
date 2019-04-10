using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;

namespace ParkMate.ApplicationServices.Commands
{
    public class RegisterCustomerCommand : IRequest<CommandResult>
    { 
        public RegisterCustomerCommand(string identityId, string email)
        {
            IdentityId = identityId;
            Email = email;
        }
        public string IdentityId { get; }
        public string Email { get; }
    }

    public class RegisterCustomerCommandHandler
        : IRequestHandler<RegisterCustomerCommand, CommandResult>
    {
        private ICustomerRepository _repository;
        private IMediator _mediator;

        public RegisterCustomerCommandHandler(
            ICustomerRepository repository,
            IMediator mediator)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(
            RegisterCustomerCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var customer = new Customer(command.IdentityId, command.Email);

            await _repository.AddAsync(customer);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new NewCustomerRegisteredEvent(customer));

            return new CommandResult(true, "Customer successfully added");
        }
    }
}
