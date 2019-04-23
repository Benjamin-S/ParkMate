using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Events;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class CreateHourlyBookingCommand : IRequest<Result>
    {
        public CreateHourlyBookingCommand(
            string customerId, 
            int vehicleId, 
            int parkingSpaceId,
            BookingPeriodDTO bookingPeriod)
        {
            CustomerId = customerId;
            VehicleId = vehicleId;
            ParkingSpaceId = parkingSpaceId;
            BookingPeriod = bookingPeriod;
        }
        public string CustomerId { get; }
        public int VehicleId { get; }
        public int ParkingSpaceId { get; }
        public BookingPeriodDTO BookingPeriod { get; } 
    }
    
    public class CreateHourlyBookingCommandHandler
        : IRequestHandler<CreateHourlyBookingCommand, Result>
    {
        private ICustomerRepository _customerRepository;
        private IParkingSpaceRepository _parkingSpaceRepository;
        private IMediator _mediator;

        public CreateHourlyBookingCommandHandler(
            ICustomerRepository customerRepository,
            IParkingSpaceRepository parkingSpaceRepository,
            IMediator mediator)
        {
            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));
            _parkingSpaceRepository = parkingSpaceRepository ??
                throw new ArgumentNullException(nameof(parkingSpaceRepository));
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result> Handle(
            CreateHourlyBookingCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var bookingPeriod = BookingInfo.CreateHourlyBooking(
                command.BookingPeriod.Start,
                command.BookingPeriod.End,
                new Money(command.BookingPeriod.Rate));
               
            
            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
            var parkingSpace = await _parkingSpaceRepository.GetByIdAsync(command.ParkingSpaceId);
            var vehicle = customer.Vehicles.SingleOrDefault(v => v.Id == command.VehicleId);

            if(!parkingSpace.IsAvailable(bookingPeriod))
            {
                return Result.CommandFail("Parking Space not available during requested period");
            }
            var booking = new Booking(customer.IdentityId, parkingSpace, vehicle, bookingPeriod);

            parkingSpace.AddBookingToSchedule(booking);
            customer.AddBooking(booking);

            _customerRepository.Update(customer);
            _parkingSpaceRepository.Update(parkingSpace);

            await _customerRepository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new NewBookingCreatedEvent(customer, parkingSpace));

            return Result.CommandSuccess("Parking Space successfully booked");
        }
    }
    
}
