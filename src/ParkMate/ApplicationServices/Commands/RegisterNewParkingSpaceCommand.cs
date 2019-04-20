using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Events;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class RegisterNewParkingSpaceCommand : IRequest<Result>
    {
        public RegisterNewParkingSpaceCommand(
            ParkingSpaceDTO parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }

        public ParkingSpaceDTO ParkingSpace { get; set; }
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
            var parkingSpace = new ParkingSpace(
                command.ParkingSpace.OwnerId,

                new ParkingSpaceDescription(
                    command.ParkingSpace.Description.Title,
                    command.ParkingSpace.Description.Description, 
                    command.ParkingSpace.Description.ImageURL),

                new Address(
                    command.ParkingSpace.Address.Street, 
                    command.ParkingSpace.Address.City, 
                    command.ParkingSpace.Address.State,
                    command.ParkingSpace.Address.Zip, 
                    new Point(
                        command.ParkingSpace.Address.Latitude, 
                        command.ParkingSpace.Address.Longitude)),

                SpaceAvailability.Create247Availability(),

                new BookingRate(
                    new Money(command.ParkingSpace.BookingRate.HourlyRate),
                    new Money(command.ParkingSpace.BookingRate.DailyRate)));

            var customer = await _customerRepository.GetByIdAsync(command.ParkingSpace.OwnerId);

            customer.ParkingSpaces.Add(parkingSpace);

            _customerRepository.Update(customer);

            await _customerRepository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));
            await _mediator.Publish(new CustomerUpdatedEvent(customer));

            return Result.CommandSuccess("Parking Space was successfully registered");
        }
    }
}