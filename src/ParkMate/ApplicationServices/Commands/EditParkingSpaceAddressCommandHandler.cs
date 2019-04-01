using System;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using System.Threading;
using ParkMate.ApplicationServices;

namespace ApplicationServices.Commands
{
    public class EditParkingSpaceAddressCommandHandler 
        : IRequestHandler<EditParkingSpaceAddressCommand, CommandResult>
    {
        private IRepository<ParkingSpace> _repository;

        public EditParkingSpaceAddressCommandHandler(IRepository<ParkingSpace> repository)
        {
            _repository = repository ?? 
                          throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CommandResult> Handle(
            EditParkingSpaceAddressCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            parkingSpace.UpdateAddress(command.Address);
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            
            return new CommandResult(true, "Parking Space address was successfully updated");
        }
    }
}