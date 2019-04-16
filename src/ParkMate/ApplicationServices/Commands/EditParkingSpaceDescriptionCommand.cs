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
    public class EditParkingSpaceDescriptionCommand  : IRequest<Result>
    {
        public EditParkingSpaceDescriptionCommand(int parkingSpaceId, string ownerId, ParkingSpaceDescription description)
        {
            ParkingSpaceId = parkingSpaceId;
            OwnerId = ownerId;
            Description = description;
        }
        public int ParkingSpaceId { get; }
        public string OwnerId { get; }
        public ParkingSpaceDescription Description { get; }
    }
    
    public class EditParkingSpaceDescriptionCommandHandler 
        : IRequestHandler<EditParkingSpaceDescriptionCommand, Result>
    {
        private IParkingSpaceRepository _repository;
        private IMediator _mediator;

        public EditParkingSpaceDescriptionCommandHandler(
            IParkingSpaceRepository repository,
            IMediator mediator)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<Result> Handle(
            EditParkingSpaceDescriptionCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            if (!parkingSpace.OwnerId.Equals(command.OwnerId))
            {
                return Result.CommandFail("Not authorized to modify this Parking Space");
            }

            parkingSpace.UpdateDescription(command.Description);
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));

            return Result.CommandSuccess("Parking Space description was successfully updated");
        }
    }
}