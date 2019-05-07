using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Util;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetHistoricalBookingsForParkingSpaceQuery
        : IRequest<Result<IReadOnlyList<BookingViewModel>>>
    {
        public GetHistoricalBookingsForParkingSpaceQuery(int parkingSpaceId)
        {
            ParkingSpaceId = parkingSpaceId;
        }
        public int ParkingSpaceId { get; set; }
    }

    public class GetHistoricalBookingsForParkingSpaceQueryHandler
        : IRequestHandler<GetHistoricalBookingsForParkingSpaceQuery,
            Result<IReadOnlyList<BookingViewModel>>>
    {
        private IMongoContext _context;

        public GetHistoricalBookingsForParkingSpaceQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<IReadOnlyList<BookingViewModel>>> Handle(
            GetHistoricalBookingsForParkingSpaceQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.Bookings.FindAsync(b =>
                b.ParkingSpace.ParkingSpaceId == query.ParkingSpaceId &&
                b.End < SystemTime.Now())
                .Result.ToListAsync();

            if (result != null && result.Count != 0)
            {
                return Result<IReadOnlyList<BookingViewModel>>.QuerySuccess(result);
            }
            return Result<IReadOnlyList<BookingViewModel>>.QueryFail("No bookings found");
        }
    }
}
