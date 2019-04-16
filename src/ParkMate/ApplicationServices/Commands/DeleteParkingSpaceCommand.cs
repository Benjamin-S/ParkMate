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
    public class DeleteParkingSpaceCommand : IRequest<Result>
    {
        public DeleteParkingSpaceCommand(int parkingSpaceId, string ownerId)
        {
            ParkingSpaceId = parkingSpaceId;
            OwnerId = ownerId;
        }
        public int ParkingSpaceId { get; }
        public string OwnerId { get; }
    }

    public class DeleteParkingSpaceCommandHandler
        : IRequestHandler<DeleteParkingSpaceCommand, Result>
    {
        private IParkingSpaceRepository _repository;
        private IMediator _mediator;

        public DeleteParkingSpaceCommandHandler(
            IParkingSpaceRepository repository,
            IMediator mediator)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<Result> Handle(
            DeleteParkingSpaceCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            if(!parkingSpace.OwnerId.Equals(command.OwnerId))
            {
                return Result.CommandFail("Not authorized to modify this Parking Space");
            }

            _repository.Delete(parkingSpace);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceDeletedEvent(parkingSpace));

            return Result.CommandSuccess("Parking Space address was successfully updated");
        }
    }
}
