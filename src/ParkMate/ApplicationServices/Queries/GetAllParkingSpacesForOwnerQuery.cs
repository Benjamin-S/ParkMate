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
    public class GetAllParkingSpacesForOwnerQuery 
        : IRequest<QueryResult<IReadOnlyList<ParkingSpace>>>
    {
        public GetAllParkingSpacesForOwnerQuery(string ownerId)
        {
            OwnerId = ownerId;
        }
        public string OwnerId { get; set; }
    }

    public class GetAllParkingSpacesForOwnerQueryHandler 
        : IRequestHandler<GetAllParkingSpacesForOwnerQuery, 
            QueryResult<IReadOnlyList<ParkingSpace>>>
    {
        private ParkMateDbContext _context;

        public GetAllParkingSpacesForOwnerQueryHandler(ParkMateDbContext context)
        {
            _context = context;
        }

        public async Task<QueryResult<IReadOnlyList<ParkingSpace>>> Handle(
            GetAllParkingSpacesForOwnerQuery query, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.ParkingSpaces
                .Include(s => s.Availability)
                .Where(s => s.OwnerId.Equals(query.OwnerId))
                .ToListAsync();

            if (result.Count != 0)
            {
                return new QueryResult<IReadOnlyList<ParkingSpace>>
                {
                    Success = true,
                    PayLoad = result
                };
            }

            return new QueryResult<IReadOnlyList<ParkingSpace>>
            {
                Success = false,
                Message = "No parking spaces found for user"
            };
        }
    }
}
