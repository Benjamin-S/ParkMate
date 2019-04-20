using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetSingleParkingSpaceQuery
        : IRequest<Result<ParkingSpaceViewModel>>
    {
        public GetSingleParkingSpaceQuery(int parkingSpaceId)
        {
            ParkingSpaceId = parkingSpaceId;
        }
        public int ParkingSpaceId { get; }
    }

    public class GetSingleParkingSpaceQueryHandler
        : IRequestHandler<GetSingleParkingSpaceQuery, Result<ParkingSpaceViewModel>>
    {
        private IMongoContext _context;

        public GetSingleParkingSpaceQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<ParkingSpaceViewModel>> Handle(
            GetSingleParkingSpaceQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var space = await _context.ParkingSpaces
                .FindAsync(ps => ps.ParkingSpaceId == query.ParkingSpaceId)
                .Result.FirstOrDefaultAsync();

            if (space != null)
            {
                return Result<ParkingSpaceViewModel>.QuerySuccess(space);
            }
            return Result<ParkingSpaceViewModel>.QueryFail("Parking space not found");
        }
    }
}
