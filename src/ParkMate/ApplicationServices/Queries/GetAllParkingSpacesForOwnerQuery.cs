using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetAllParkingSpacesForOwnerQuery 
        : IRequest<Result<IReadOnlyList<ParkingSpaceViewModel>>>
    {
        public GetAllParkingSpacesForOwnerQuery(string ownerId)
        {
            OwnerId = ownerId;
        }
        public string OwnerId { get; set; }
    }

    public class GetAllParkingSpacesForOwnerQueryHandler 
        : IRequestHandler<GetAllParkingSpacesForOwnerQuery, 
            Result<IReadOnlyList<ParkingSpaceViewModel>>>
    {
        private IMongoContext _context;

        public GetAllParkingSpacesForOwnerQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<IReadOnlyList<ParkingSpaceViewModel>>> Handle(
            GetAllParkingSpacesForOwnerQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.ParkingSpaces.FindAsync(o => o.OwnerId == query.OwnerId).Result.ToListAsync();

            if (result != null && result.Count != 0)
            {
                return Result<IReadOnlyList<ParkingSpaceViewModel>>.QuerySuccess(result);
            }
            return Result<IReadOnlyList<ParkingSpaceViewModel>>.QueryFail("Parking space not found");
        }
    }
}
