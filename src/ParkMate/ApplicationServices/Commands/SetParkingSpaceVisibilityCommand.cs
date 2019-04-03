using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Commands
{
    public class SetParkingSpaceVisibilityCommand  : IRequest<CommandResult>
    {
        public SetParkingSpaceVisibilityCommand(int parkingSpaceId, bool isListed)
        {
            ParkingSpaceId = parkingSpaceId;
            IsListed = isListed;
        }
        public int ParkingSpaceId { get; }
        public bool IsListed { get; }
    }
    
    public class SetParkingSpaceVisibilityCommandCommandHandler 
        : IRequestHandler<SetParkingSpaceVisibilityCommand, CommandResult>
    {
        private IRepository<ParkingSpace> _repository;

        public SetParkingSpaceVisibilityCommandCommandHandler(IRepository<ParkingSpace> repository)
        {
            _repository = repository ?? 
                          throw new ArgumentNullException(nameof(repository));
        }

        public async Task<CommandResult> Handle(
            SetParkingSpaceVisibilityCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            parkingSpace.SetVisibility(command.IsListed);
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();
            
            return new CommandResult(true, "Parking Space has been " +
                (command.IsListed ? "publicly listed" : "unlisted"));
        }
    }
}