
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
    public class GetHistoricalBookingsForCustomerQuery
        : IRequest<Result<IReadOnlyList<BookingViewModel>>>
    {
        public GetHistoricalBookingsForCustomerQuery(string customerId)
        {
            CustomerId = customerId;
        }
        public string CustomerId { get; set; }
    }

    public class GetHistoricalBookingsForCustomerQueryHandler
        : IRequestHandler<GetHistoricalBookingsForCustomerQuery,
            Result<IReadOnlyList<BookingViewModel>>>
    {
        private IMongoContext _context;

        public GetHistoricalBookingsForCustomerQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<IReadOnlyList<BookingViewModel>>> Handle(
            GetHistoricalBookingsForCustomerQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.Bookings.FindAsync(b => 
                b.CustomerId.Equals(query.CustomerId) &&
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
