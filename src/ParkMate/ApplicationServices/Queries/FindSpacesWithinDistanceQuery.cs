using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Queries
{
    public class FindSpacesWithinDistanceQuery 
        : IRequest<Result<IReadOnlyList<ParkingSpaceListingDTO>>>
    {
        public FindSpacesWithinDistanceQuery(DistanceSearchDTO query)
        {
            Paramaters = query;
        }
        public DistanceSearchDTO Paramaters { get; set; }
    }

    public class FindSpacesWithinDistanceQueryHandler
    : IRequestHandler<FindSpacesWithinDistanceQuery,
        Result<IReadOnlyList<ParkingSpaceListingDTO>>>
    {
        private IMongoContext _context;

        public FindSpacesWithinDistanceQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<IReadOnlyList<ParkingSpaceListingDTO>>> Handle(
            FindSpacesWithinDistanceQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var point = GeoJson.Point(
                GeoJson.Geographic(query.Paramaters.Longitude, query.Paramaters.Latitude));

            var locationQuery = new FilterDefinitionBuilder<ParkingSpaceListingDTO>()
                .Near(ps => ps.Location, point, query.Paramaters.DistanceInMeters);

            var result = await _context.ParkingSpaceListings.FindAsync(locationQuery).Result.ToListAsync();

            if (result != null && result.Count != 0)
            {
                return Result<IReadOnlyList<ParkingSpaceListingDTO>>.QuerySuccess(result);
            }
            return Result<IReadOnlyList<ParkingSpaceListingDTO>>.QueryFail("Parking space not found");
        }
    }
}
