using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;

namespace ParkMate.ApplicationServices.Commands
{
    public class SetParkingSpaceVisibilityCommand  : IRequest<Result>
    {
        public SetParkingSpaceVisibilityCommand(int parkingSpaceId, string ownerId, bool isListed)
        {
            ParkingSpaceId = parkingSpaceId;
            OwnerId = ownerId;
            IsListed = isListed;
        }
        public int ParkingSpaceId { get; }
        public string OwnerId { get; }
        public bool IsListed { get; }
    }
    
    public class SetParkingSpaceVisibilityCommandHandler 
        : IRequestHandler<SetParkingSpaceVisibilityCommand, Result>
    {
        private IParkingSpaceRepository _repository;
        private IMediator _mediator;

        public SetParkingSpaceVisibilityCommandHandler(
            IParkingSpaceRepository repository,
            IMediator mediator)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<Result> Handle(
            SetParkingSpaceVisibilityCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            if (!parkingSpace.OwnerId.Equals(command.OwnerId))
            {
                return Result.CommandFail("Not authorized to modify this Parking Space");
            }

            parkingSpace.SetVisibility(command.IsListed);
            
            _repository.Update(parkingSpace);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceUpdatedEvent(parkingSpace));

            return Result.CommandSuccess("Parking Space has been " +
                (command.IsListed ? "publicly listed" : "unlisted"));
        }
    }
}