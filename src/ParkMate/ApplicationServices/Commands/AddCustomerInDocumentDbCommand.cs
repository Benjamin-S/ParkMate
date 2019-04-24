using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class AddCustomerInDocumentDbCommand : IRequest<Result>
    {
        public AddCustomerInDocumentDbCommand(
            Customer customer)
        {
            Customer = customer;
        }
        public Customer Customer { get; }
    }

    public class AddCustomerInDocumentDbCommandHandler
        : IRequestHandler<AddCustomerInDocumentDbCommand, Result>
    {
        private IMongoContext _context;
        private IMapper _mapper;

        public AddCustomerInDocumentDbCommandHandler(
            IMongoContext context,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(
            AddCustomerInDocumentDbCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var customer = _mapper.Map<Customer, CustomerViewModel>(command.Customer);

            await _context.Customers.InsertOneAsync(customer);

            return Result.Ok();
        }
    }
}