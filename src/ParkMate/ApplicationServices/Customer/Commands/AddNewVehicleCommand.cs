using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class AddNewVehicleCommand : IRequest<Result>
    {
        public string CustomerId { get; }
        public VehicleDTO Vehicle { get; }
        
        public AddNewVehicleCommand(string customerId, VehicleDTO vehicle)
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

            var vehicle = new Vehicle(
                command.Vehicle.Make, 
                command.Vehicle.Model, 
                command.Vehicle.Color, 
                command.Vehicle.Registration);

            customer.Vehicles.Add(vehicle);

            _repository.Update(customer);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new CustomerUpdatedEvent(customer));

            return Result.CommandSuccess("Vehicle successfully registered");
        }
    }
}
