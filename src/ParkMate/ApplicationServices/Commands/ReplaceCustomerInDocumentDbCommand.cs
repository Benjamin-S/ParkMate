using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using MongoDB.Driver;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class ReplaceCustomerInDocumentDbCommand : IRequest<Result>
    {
        public ReplaceCustomerInDocumentDbCommand(Customer customer)
        {
            Customer = customer;
        }
        public Customer Customer { get; }
    }

    public class ReplaceCustomerInDocumentDbCommandHandler
        : IRequestHandler<ReplaceCustomerInDocumentDbCommand, Result>
    {
        private IMongoContext _context;
        private IMapper _mapper;

        public ReplaceCustomerInDocumentDbCommandHandler(
            IMongoContext context,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(
            ReplaceCustomerInDocumentDbCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var customer = _mapper.Map<Customer, CustomerViewModel>(command.Customer);

            await _context.Customers.ReplaceOneAsync(c => 
                c.CustomerId.Equals(command.Customer.IdentityId), 
                customer,
                new UpdateOptions { IsUpsert = true }); 

            return Result.Ok();
        }
    }
}