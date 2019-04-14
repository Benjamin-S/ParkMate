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
        : IRequest<Result<ParkingSpace>>
    {
        public GetSingleParkingSpaceQuery(int parkingSpaceId)
        {
            ParkingSpaceId = parkingSpaceId;
        }
        public int ParkingSpaceId { get; }
    }

    public class GetSingleParkingSpaceQueryHandler
        : IRequestHandler<GetSingleParkingSpaceQuery, Result<ParkingSpace>>
    {
        private IMongoContext _context;

        public GetSingleParkingSpaceQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<ParkingSpace>> Handle(
            GetSingleParkingSpaceQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<ParkingSpace>.Filter.Eq(ps => ps.Id, query.ParkingSpaceId);

            var space = await _context.ParkingSpaces.FindAsync(filter).Result.FirstOrDefaultAsync();

            if (space != null)
            {
                return Result<ParkingSpace>.QuerySuccess(space);
            }
            return Result<ParkingSpace>.QueryFail("Parking space not found");
        }
    }
}
