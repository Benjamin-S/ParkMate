using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetSingleParkingSpaceQuery
        : IRequest<QueryResult<ParkingSpace>>
    {
        public GetSingleParkingSpaceQuery(int parkingSpaceId)
        {
            ParkingSpaceId = parkingSpaceId;
        }
        public int ParkingSpaceId { get; }
    }

    public class GetSingleParkingSpaceQueryHandler
        : IRequestHandler<GetSingleParkingSpaceQuery, QueryResult<ParkingSpace>>
    {
        private IMongoContext _context;

        public GetSingleParkingSpaceQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<QueryResult<ParkingSpace>> Handle(
            GetSingleParkingSpaceQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<ParkingSpace>.Filter
                .Eq(ps => ps.Id == query.ParkingSpaceId, true);

            var space = await _context.ParkingSpaces.FindAsync(filter).Result.FirstOrDefaultAsync();

            if (space != null)
            {
                return new QueryResult<ParkingSpace>
                {
                    Success = true,
                    PayLoad = space
                };
            }
            return new QueryResult<ParkingSpace>
            {
                Success = false,
                Message = "Parking space not found"
            };
        }
    }
}
