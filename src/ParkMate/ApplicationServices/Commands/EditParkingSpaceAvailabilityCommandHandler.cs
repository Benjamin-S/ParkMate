using System;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using System.Threading;
using ParkMate.ApplicationServices;

namespace ApplicationServices.Commands
{
    public class EditParkingSpaceAvailabilityCommandHandler 
        : IRequestHandler<EditParkingSpaceAvailabilityCommand, CommandResult>
    {
        private IRepository<ParkingSpace> _repository;

        public EditParkingSpaceAvailabilityCommandHandler(IRepository<ParkingSpace> repository)
        {
            _repository = repository ?? 
                          throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CommandResult> Handle(
            EditParkingSpaceAvailabilityCommand command, 
            CancellationToken cancellationToken)
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            foreach (var time in command.AvailabilityTimes)
            {
                parkingSpace.Availability.SetAvailabilityForDay(time);
            }
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            
            return new CommandResult(true, "Parking Space availability was successfully updated");
        }
    }
}