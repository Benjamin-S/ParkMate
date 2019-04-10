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
    public class EditParkingSpaceDescriptionCommand  : IRequest<CommandResult>
    {
        public EditParkingSpaceDescriptionCommand(int parkingSpaceId, ParkingSpaceDescription description)
        {
            ParkingSpaceId = parkingSpaceId;
            Description = description;
        }
        public int ParkingSpaceId { get; }
        public ParkingSpaceDescription Description { get; }
    }
    
    public class EditParkingSpaceDescriptionCommandHandler 
        : IRequestHandler<EditParkingSpaceDescriptionCommand, CommandResult>
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

        public async Task<CommandResult> Handle(
            EditParkingSpaceDescriptionCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            parkingSpace.UpdateDescription(command.Description);
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));

            return new CommandResult(true, "Parking Space description was successfully updated");
        }
    }
}