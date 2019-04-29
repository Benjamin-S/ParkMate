using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Queries
{
    public class GetCustomerQuery
        : IRequest<Result<CustomerViewModel>>
    {
        public GetCustomerQuery(string customerId)
        {
            CustomerId = customerId;
        }
        public string CustomerId { get; }
    }

    public class GetCustomerQueryHandler
        : IRequestHandler<GetCustomerQuery, Result<CustomerViewModel>>
    {
        private IMongoContext _context;

        public GetCustomerQueryHandler(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Result<CustomerViewModel>> Handle(
            GetCustomerQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var customer = await _context.Customers.FindAsync(c => 
                c.CustomerId.Equals(query.CustomerId))
                .Result.FirstOrDefaultAsync();

            if (customer != null)
            {
                return Result<CustomerViewModel>.QuerySuccess(customer);
            }
            return Result<CustomerViewModel>.QueryFail("Customer not found");
        }
    }
}
