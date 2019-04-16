using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Events;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Commands
{
    public class BookParkingSpaceCommand : IRequest<Result>
    {
        public BookParkingSpaceCommand(
            string customerId, 
            int vehicleId, 
            int parkingSpaceId,
            BookingPeriod bookingPeriod)
        {
            CustomerId = customerId;
            VehicleId = vehicleId;
            ParkingSpaceId = parkingSpaceId;
            BookingPeriod = bookingPeriod;
        }
        public string CustomerId { get; }
        public int VehicleId { get; }
        public int ParkingSpaceId { get; }
        public BookingPeriod BookingPeriod { get; } 
    }
    
    public class BookParkingSpaceCommandHandler
        : IRequestHandler<BookParkingSpaceCommand, Result>
    {
        private ICustomerRepository _customerRepository;
        private IParkingSpaceRepository _parkingSpaceRepository;
        private IMediator _mediator;

        public BookParkingSpaceCommandHandler(
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
            BookParkingSpaceCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var customer = await _customerRepository.GetByIdAsync(command.CustomerId);
            var parkingSpace = await _parkingSpaceRepository.GetByIdAsync(command.ParkingSpaceId);
            var vehicle = customer.Vehicles.SingleOrDefault(v => v.Id == command.VehicleId);

            if(!parkingSpace.IsAvailable(command.BookingPeriod))
            {
                return Result.CommandFail("Parking Space not available during requested period");
            }
            var booking = new Booking(parkingSpace, vehicle, command.BookingPeriod);

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
