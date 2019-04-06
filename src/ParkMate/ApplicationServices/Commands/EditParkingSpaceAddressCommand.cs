using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;

namespace ParkMate.ApplicationServices.Commands
{
    public class EditParkingSpaceAddressCommand  : IRequest<CommandResult>
    {
        public EditParkingSpaceAddressCommand(int parkingSpaceId, Address address)
        {
            ParkingSpaceId = parkingSpaceId;
            Address = address;
        }
        public int ParkingSpaceId { get; }
        public Address Address { get; }
    }
    
    public class EditParkingSpaceAddressCommandHandler 
        : IRequestHandler<EditParkingSpaceAddressCommand, CommandResult>
    {
        private IRepository<ParkingSpace> _repository;
        private IMediator _mediator;

        public EditParkingSpaceAddressCommandHandler(
            IRepository<ParkingSpace> repository,
            IMediator mediator)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(
            EditParkingSpaceAddressCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            parkingSpace.UpdateAddress(command.Address);
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));

            return new CommandResult(true, "Parking Space address was successfully updated");
        }
    }
}
