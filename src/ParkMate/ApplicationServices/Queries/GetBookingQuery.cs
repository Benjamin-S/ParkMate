using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetBookingQuery
        : IRequest<Result<BookingViewModel>>
    {
        public GetBookingQuery(int bookingId)
        {
            BookingId = bookingId;
        }
        public int BookingId { get; }
    }

    public class GetBookingQueryHandler
        : IRequestHandler<GetBookingQuery, Result<BookingViewModel>>
    {
        private IMongoContext _context;

        public GetBookingQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<BookingViewModel>> Handle(
            GetBookingQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var booking = await _context.Bookings
                .FindAsync(b => b.BookingId == query.BookingId)
                .Result.FirstOrDefaultAsync();

            if (booking != null)
            {
                return Result<BookingViewModel>.QuerySuccess(booking);
            }
            return Result<BookingViewModel>.QueryFail("Booking not found");
        }
    }
}
