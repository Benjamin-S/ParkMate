using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetAllParkingSpacesForOwnerQuery 
        : IRequest<Result<IReadOnlyList<ParkingSpace>>>
    {
        public GetAllParkingSpacesForOwnerQuery(string ownerId)
        {
            OwnerId = ownerId;
        }
        public string OwnerId { get; set; }
    }

    public class GetAllParkingSpacesForOwnerQueryHandler 
        : IRequestHandler<GetAllParkingSpacesForOwnerQuery, 
            Result<IReadOnlyList<ParkingSpace>>>
    {
        private IMongoContext _context;

        public GetAllParkingSpacesForOwnerQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<IReadOnlyList<ParkingSpace>>> Handle(
            GetAllParkingSpacesForOwnerQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<ParkingSpace>.Filter.Eq(o => o.OwnerId, query.OwnerId);

            var result = await _context.ParkingSpaces.FindAsync(filter).Result.ToListAsync();

            if (result != null && result.Count != 0)
            {
                return Result<IReadOnlyList<ParkingSpace>>.QuerySuccess(result);
            }
            return Result<IReadOnlyList<ParkingSpace>>.QueryFail("Parking space not found");
        }
    }
}
