using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using MongoDB.Driver;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetCustomerQuery
        : IRequest<Result<Customer>>
    {
        public GetCustomerQuery(string customerId)
        {
            CustomerId = customerId;
        }
        public string CustomerId { get; }
    }

    public class GetCustomerQueryHandler
        : IRequestHandler<GetCustomerQuery, Result<Customer>>
    {
        private IMongoContext _context;

        public GetCustomerQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<Customer>> Handle(
            GetCustomerQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.IdentityId, query.CustomerId);

            var customer = await _context.Customers.FindAsync(filter).Result.FirstOrDefaultAsync();

            if (customer != null)
            {
                return Result<Customer>.QuerySuccess(customer);
            }
            return Result<Customer>.QueryFail("Customer not found");
        }
    }
}
