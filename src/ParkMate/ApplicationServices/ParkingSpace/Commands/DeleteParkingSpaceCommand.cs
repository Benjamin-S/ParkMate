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
        private IParkingSpaceRepository _parkingSpaceRepository;
        private ICustomerRepository _customerRepository;
        private IMediator _mediator;

        public DeleteParkingSpaceCommandHandler(
            IParkingSpaceRepository parkingSpaceRepository,
            ICustomerRepository customerRepository,
            IMediator mediator)
        {
            _parkingSpaceRepository = parkingSpaceRepository ??
                throw new ArgumentNullException(nameof(parkingSpaceRepository));
            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));
            _mediator = mediator;
        }

        public async Task<Result> Handle(
            DeleteParkingSpaceCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _parkingSpaceRepository.GetByIdAsync(command.ParkingSpaceId);
            var customer = await _customerRepository.GetByIdAsync(command.OwnerId);

            if (!parkingSpace.OwnerId.Equals(command.OwnerId))
            {
                return Result.CommandFail("Not authorized to modify this Parking Space");
            }

            _parkingSpaceRepository.Delete(parkingSpace);

            await _parkingSpaceRepository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceDeletedEvent(parkingSpace));
            await _mediator.Publish(new CustomerUpdatedEvent(customer));

            return Result.CommandSuccess("Parking Space address was successfully updated");
        }
    }
}
