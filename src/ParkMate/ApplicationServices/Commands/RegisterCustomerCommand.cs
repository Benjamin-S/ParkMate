using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;

namespace ParkMate.ApplicationServices.Commands
{
    public class RegisterCustomerCommand : IRequest<Result>
    { 
        public RegisterCustomerCommand(string identityId, string email, string name)
        {
            IdentityId = identityId;
            Email = email;
            Name = name;
        }
        public string IdentityId { get; }
        public string Email { get; }
        public string Name { get; }
    }

    public class RegisterCustomerCommandHandler
        : IRequestHandler<RegisterCustomerCommand, Result>
    {
        private ICustomerRepository _repository;
        private IMediator _mediator;

        public RegisterCustomerCommandHandler(
            ICustomerRepository repository,
            IMediator mediator)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator  ??
                throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result> Handle(
            RegisterCustomerCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var customer = new Customer(command.IdentityId, command.Email, command.Name);

            await _repository.AddAsync(customer);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new NewCustomerRegisteredEvent(customer));

            return Result.CommandSuccess("Customer successfully added");
        }
    }
}
