using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;

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
        private IMediator _mediator;

        public SetParkingSpaceVisibilityCommandCommandHandler(
            IRepository<ParkingSpace> repository,
            IMediator mediator)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(
            SetParkingSpaceVisibilityCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            parkingSpace.SetVisibility(command.IsListed);
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));

            return new CommandResult(true, "Parking Space has been " +
                (command.IsListed ? "publicly listed" : "unlisted"));
        }
    }
}