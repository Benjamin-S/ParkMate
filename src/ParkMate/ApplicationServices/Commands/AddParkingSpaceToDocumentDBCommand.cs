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
    public class AddParkingSpaceToDocumentDBCommand : IRequest<Result>
    {
        public AddParkingSpaceToDocumentDBCommand(
            ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }
        public ParkingSpace ParkingSpace { get; }
    }

    public class AddParkingSpaceToDocumentDBCommandHandler
        : IRequestHandler<AddParkingSpaceToDocumentDBCommand, Result>
    {
        private IMongoContext _context;
        private IMapper _mapper;

        public AddParkingSpaceToDocumentDBCommandHandler(
            IMongoContext context,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(
            AddParkingSpaceToDocumentDBCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var listing = _mapper.Map<ParkingSpaceListingDTO>(command.ParkingSpace);
            
            await _context.ParkingSpaces.InsertOneAsync(command.ParkingSpace);
            await _context.ParkingSpaceListings.InsertOneAsync(listing);

            return Result.Ok();
        }
    }
}