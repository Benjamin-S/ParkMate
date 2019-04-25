using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Util;

namespace ParkMate.ApplicationServices.Queries
{
    public class FindSpacesWithinDistanceQuery
        : IRequest<Result<IReadOnlyList<ParkingSpaceViewModel>>>
    {
        public FindSpacesWithinDistanceQuery(DistanceSearchDTO query)
        {
            Paramaters = query;
        }
        public DistanceSearchDTO Paramaters { get; set; }
    }

    public class FindSpacesWithinDistanceQueryHandler
    : IRequestHandler<FindSpacesWithinDistanceQuery,
        Result<IReadOnlyList<ParkingSpaceViewModel>>>
    {
        private IMongoContext _context;

        public FindSpacesWithinDistanceQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<IReadOnlyList<ParkingSpaceViewModel>>> Handle(
            FindSpacesWithinDistanceQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var point = GeoJson.Point(
                GeoJson.Geographic(query.Paramaters.Longitude, query.Paramaters.Latitude));

            var locationQuery = new FilterDefinitionBuilder<ParkingSpaceViewModel>()
                .Near(ps => ps.Location, point, query.Paramaters.DistanceInMeters);

            var results = await _context.ParkingSpaces.FindAsync(locationQuery).Result.ToListAsync();

            foreach(var result in results)
            {
                result.DistanceInMeters = (int) Distance.Haversine(
                    new Point(query.Paramaters.Latitude, query.Paramaters.Longitude),                     new Point(result.Location.Coordinates.Latitude, result.Location.Coordinates.Longitude));
            }
            if (results.Count != 0)
            {
                return Result<IReadOnlyList<ParkingSpaceViewModel>>.QuerySuccess(results);
            }
            return Result<IReadOnlyList<ParkingSpaceViewModel>>.QueryFail("Parking space not found");
        }
    }
}