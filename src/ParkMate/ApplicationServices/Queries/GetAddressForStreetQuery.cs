using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;
using Microsoft.Extensions.Configuration;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetAddressForStreetQuery
        : IRequest<Result<IEnumerable<SearchAddressDTO>>>
    {
        public GetAddressForStreetQuery(string partialStreet)
        {
            PartialStreet = partialStreet;
        }
        public string PartialStreet { get; }
    }
    public class GetAddressForStreetQueryHandler
        : IRequestHandler<GetAddressForStreetQuery, Result<IEnumerable<SearchAddressDTO>>>
    {
        private readonly IConfiguration _configuration;

        public GetAddressForStreetQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<IEnumerable<SearchAddressDTO>>> Handle(
            GetAddressForStreetQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            IEnumerable<SearchAddressDTO> results;

            using (var connection = new NpgsqlConnection(_configuration["ConnectionStrings:ParkMateDB"]))
            {

                string sQuery = "SELECT * FROM \"SearchAddresses\" WHERE \"Street\" ILIKE CONCAT('%', @Qs, '%') ORDER BY \"Street\" ASC LIMIT 10;";
                connection.Open();
                results = await connection.QueryAsync<SearchAddressDTO>(sQuery, new { Qs = query.PartialStreet });
            }

            if (results != null)
            {
                return Result<IEnumerable<SearchAddressDTO>>.QuerySuccess(results);
            }
            return Result<IEnumerable<SearchAddressDTO>>.QueryFail("No addresses match query");
        }
    }
}
