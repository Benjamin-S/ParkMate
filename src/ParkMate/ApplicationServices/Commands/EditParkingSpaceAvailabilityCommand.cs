using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class EditParkingSpaceAvailabilityCommand  : IRequest<Result>
    {
        public EditParkingSpaceAvailabilityCommand(
            int parkingSpaceId, 
            string ownerId, 
            List<AvailableTimeDTO> times)
        {
            ParkingSpaceId = parkingSpaceId;
            OwnerId = ownerId;
            AvailabilityTimes = times;
        }

        public int ParkingSpaceId { get; }
        public string OwnerId { get; }
        public IReadOnlyList<AvailableTimeDTO> AvailabilityTimes { get; }
    }

    public class EditParkingSpaceAvailabilityCommandHandler 
        : IRequestHandler<EditParkingSpaceAvailabilityCommand, Result>
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

        public async Task<Result> Handle(
            EditParkingSpaceAvailabilityCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            if (!parkingSpace.OwnerId.Equals(command.OwnerId))
            {
                return Result.CommandFail("Not authorized to modify this Parking Space");
            }
            var updatedDays = command.AvailabilityTimes
                .Select(d => AvailabilityTime
                .CreateAvailabilityWithHours(d.DayOfWeek, d.AvailableFrom, d.AvailableTo))
                .ToList();

            foreach (var time in updatedDays)
            {
                parkingSpace.Availability.SetAvailabilityForDay(time);
            }
            
            _repository.Update(parkingSpace);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceUpdatedEvent(parkingSpace));

            return Result.CommandSuccess("Parking Space availability was successfully updated");
        }
    }
}