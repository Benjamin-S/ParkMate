using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;

namespace ParkMate.ApplicationServices.Commands
{
    public class AddNewVehicleCommand : IRequest<CommandResult>
    {
        public AddNewVehicleCommand(string customerId, Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
        public string CustomerId { get; }
        public Vehicle Vehicle { get; }
    }

    public class AddNewVehicleCommandHandler
        : IRequestHandler<AddNewVehicleCommand, CommandResult>
    {
        private ICustomerRepository _repository;
        private IMediator _mediator;

        public AddNewVehicleCommandHandler(
            ICustomerRepository repository,
            IMediator mediator)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(
            AddNewVehicleCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var customer = await _repository.GetByIdAsync(command.CustomerId);
            customer.Vehicles.Add(command.Vehicle);

            _repository.Update(customer);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new CustomerUpdatedEvent(customer));

            return new CommandResult(true, "Vehicle successfully registered");
        }
    }
}
