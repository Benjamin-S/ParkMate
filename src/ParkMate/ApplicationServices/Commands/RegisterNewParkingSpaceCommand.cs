using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Events;

namespace ParkMate.ApplicationServices.Commands
{
    public class RegisterNewParkingSpaceCommand : IRequest<Result>
    {
        public RegisterNewParkingSpaceCommand(
            string ownerId,
            ParkingSpaceDescription description,
            Address address,
            SpaceAvailability availability,
            BookingRate bookingRate)
        {
            OwnerId = ownerId;
            Description = description;
            Address = address;
            Availability = availability;
            BookingRate = bookingRate;
        }

        public string OwnerId { get; }
        public ParkingSpaceDescription Description { get; }
        public Address Address { get; }
        public SpaceAvailability Availability { get; }
        public BookingRate BookingRate { get; }
    }
    
    public class RegisterNewParkingSpaceCommandHandler 
        : IRequestHandler<RegisterNewParkingSpaceCommand, Result>
    {
        ICustomerRepository _customerRepository;
        private IMediator _mediator;

        public RegisterNewParkingSpaceCommandHandler(
            ICustomerRepository customerRepository,
            IMediator mediator)
        {
            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result> Handle(
            RegisterNewParkingSpaceCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = new ParkingSpace(command.OwnerId, command.Description,
                command.Address, command.Availability, command.BookingRate);

            var customer = await _customerRepository.GetByIdAsync(command.OwnerId);

            customer.ParkingSpaces.Add(parkingSpace);

            _customerRepository.Update(customer);

            await _customerRepository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));
            await _mediator.Publish(new CustomerUpdatedEvent(customer));

            return Result.CommandSuccess("Parking Space was successfully registered");
        }
    }
}