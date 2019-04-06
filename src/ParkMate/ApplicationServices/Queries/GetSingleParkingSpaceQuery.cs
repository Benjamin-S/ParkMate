using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.Infrastructure.Data;
using System.Collections.Generic;

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
        private ParkMateDbContext _context;

        public GetSingleParkingSpaceQueryHandler(ParkMateDbContext context)
        {
            _context = context;
        }

        public async Task<QueryResult<ParkingSpace>> Handle(
            GetSingleParkingSpaceQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var space = await _context.ParkingSpaces
                .Include(s => s.Availability)
                .SingleOrDefaultAsync(s => s.Id == query.ParkingSpaceId);

            if(space != null)
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
