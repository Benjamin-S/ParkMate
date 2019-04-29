using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Events;
using ParkMate.ApplicationServices.Interfaces;

namespace ApplicationServices.Commands
{
    public class CancelBookingCommand : IRequest<Result>
    {
        public CancelBookingCommand(int bookingId, string userId)
        {
            BookingId = bookingId;
            UserId = userId;
        }
        public int BookingId { get; }
        public string UserId { get; }
    }

    public class CancelBookingCommandHandler
       : IRequestHandler<CancelBookingCommand, Result>
    {
        private IBookingRepository _bookingRepository;
        private ICustomerRepository _customerRepository;
        private IMediator _mediator;

        public CancelBookingCommandHandler(
            IBookingRepository bookingRepository,
            ICustomerRepository customerRepository,
            IMediator mediator)
        {
            _bookingRepository = bookingRepository ??
                throw new ArgumentNullException(nameof(bookingRepository));
            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result> Handle(
            CancelBookingCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var booking = await _bookingRepository.GetByIdAsync(command.BookingId);

            var buyer = await _customerRepository.GetByIdAsync(booking.CustomerId);
            var seller = await _customerRepository.GetByIdAsync(booking.ParkingSpace.OwnerId);

            booking.CancelBooking();

            _bookingRepository.Update(booking);

            await _bookingRepository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new BookingCanceledEvent(buyer, seller, booking));

            return Result.CommandSuccess("Booking has been canceled");
        }
    }

}
