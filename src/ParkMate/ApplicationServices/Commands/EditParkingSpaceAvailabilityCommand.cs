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
    public class EditParkingSpaceAvailabilityCommand  : IRequest<CommandResult>
    {
        public EditParkingSpaceAvailabilityCommand(int parkingSpaceId, List<AvailabilityTime> times)
        {
            ParkingSpaceId = parkingSpaceId;
            AvailabilityTimes = times;
        }
        public int ParkingSpaceId { get; }
        public IReadOnlyList<AvailabilityTime> AvailabilityTimes { get; }
    
    }
    
    public class EditParkingSpaceAvailabilityCommandHandler 
        : IRequestHandler<EditParkingSpaceAvailabilityCommand, CommandResult>
    {
        private IParkingSpaceRepository _repository;
        private IMediator _mediator;

        public EditParkingSpaceAvailabilityCommandHandler(
            IParkingSpaceRepository repository,
            IMediator mediator)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(
            EditParkingSpaceAvailabilityCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            foreach (var time in command.AvailabilityTimes)
            {
                parkingSpace.Availability.SetAvailabilityForDay(time);
            }
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));

            return new CommandResult(true, "Parking Space availability was successfully updated");
        }
    }
}