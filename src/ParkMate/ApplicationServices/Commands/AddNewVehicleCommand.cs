using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;

namespace ParkMate.ApplicationServices.Commands
{
    public class AddNewVehicleCommand : IRequest<Result>
    {
        public string CustomerId { get; }
        public Vehicle Vehicle { get; }
        
        public AddNewVehicleCommand(string customerId, Vehicle vehicle)
        {
            CustomerId = customerId;
            Vehicle = vehicle;
        }
    }

    public class AddNewVehicleCommandHandler
        : IRequestHandler<AddNewVehicleCommand, Result>
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

        public async Task<Result> Handle(
            AddNewVehicleCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var customer = await _repository.GetByIdAsync(command.CustomerId);
            customer.Vehicles.Add(command.Vehicle);

            _repository.Update(customer);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new CustomerUpdatedEvent(customer));

            return Result.CommandSuccess("Vehicle successfully registered");
        }
    }
}
