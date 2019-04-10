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
    public class DeleteParkingSpaceCommand : IRequest<CommandResult>
    {
        public DeleteParkingSpaceCommand(int parkingSpaceId)
        {
            ParkingSpaceId = parkingSpaceId;
        }
        public int ParkingSpaceId { get; }
    }

    public class DeleteParkingSpaceCommandHandler
        : IRequestHandler<DeleteParkingSpaceCommand, CommandResult>
    {
        private IRepository<ParkingSpace> _repository;
        private IMediator _mediator;

        public DeleteParkingSpaceCommandHandler(
            IRepository<ParkingSpace> repository,
            IMediator mediator)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(
            DeleteParkingSpaceCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            _repository.Delete(parkingSpace);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceDeletedEvent(parkingSpace));

            return new CommandResult(true, "Parking Space address was successfully updated");
        }
    }
}
