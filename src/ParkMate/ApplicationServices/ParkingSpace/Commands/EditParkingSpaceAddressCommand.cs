using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class EditParkingSpaceAddressCommand  : IRequest<Result>
    {
        public EditParkingSpaceAddressCommand(int parkingSpaceId, string ownerId, AddressDTO address)
        {
            ParkingSpaceId = parkingSpaceId;
            OwnerId = ownerId;
            Address = address;
        }
        public int ParkingSpaceId { get; }
        public string OwnerId { get; }
        public AddressDTO Address { get; }
    }
    
    public class EditParkingSpaceAddressCommandHandler 
        : IRequestHandler<EditParkingSpaceAddressCommand, Result>
    {
        private IParkingSpaceRepository _repository;
        private IMediator _mediator;

        public EditParkingSpaceAddressCommandHandler(
            IParkingSpaceRepository repository,
            IMediator mediator)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<Result> Handle(
            EditParkingSpaceAddressCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            if (!parkingSpace.OwnerId.Equals(command.OwnerId))
            {
                return Result.CommandFail("Not authorized to modify this Parking Space");
            }

            var address = new Address(
                command.Address.Street, 
                command.Address.City, 
                command.Address.State, 
                command.Address.Zip, 
                new Point(command.Address.Latitude, command.Address.Longitude));

            parkingSpace.UpdateAddress(address);
            
            _repository.Update(parkingSpace);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceUpdatedEvent(parkingSpace));

            return Result.CommandSuccess("Parking Space address was successfully updated");
        }
    }
}
