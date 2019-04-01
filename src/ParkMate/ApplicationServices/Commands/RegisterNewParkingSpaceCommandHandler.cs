using System;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using System.Threading;
using ParkMate.ApplicationServices;

namespace ApplicationServices.Commands
{
    public class RegisterNewParkingSpaceCommandHandler 
        : IRequestHandler<RegisterNewParkingSpaceCommand, CommandResult>
    {
        private IRepository<ParkingSpace> _repository;

        public RegisterNewParkingSpaceCommandHandler(IRepository<ParkingSpace> repository)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CommandResult> Handle(
            RegisterNewParkingSpaceCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = new ParkingSpace(command.OwnerId, command.Description,
                command.Address, command.Availability, command.BookingRate);

            await _repository.AddAsync(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            
            return new CommandResult(true, "Parking Space was successfully registered");
        }
    }
}