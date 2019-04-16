using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.DTOs;
using MongoDB.Driver;

namespace ParkMate.ApplicationServices.Commands
{
    public class DeleteParkingSpaceFromDocumentDbCommand : IRequest<Result>
    {
        public DeleteParkingSpaceFromDocumentDbCommand(
            ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }
        public ParkingSpace ParkingSpace { get; }
    }

    public class DeleteParkingSpaceFromDocumentDbCommandHandler
        : IRequestHandler<DeleteParkingSpaceFromDocumentDbCommand, Result>
    {
        private IMongoContext _context;
        private IMapper _mapper;

        public DeleteParkingSpaceFromDocumentDbCommandHandler(
            IMongoContext context,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(
            DeleteParkingSpaceFromDocumentDbCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var filter1 = Builders<ParkingSpaceListingDTO>.Filter.Eq(ps => ps.ParkingSpaceId, command.ParkingSpace.Id);
            var filter2 = Builders<ParkingSpace>.Filter.Eq(ps => ps.Id, command.ParkingSpace.Id);
            await _context.ParkingSpaceListings.DeleteOneAsync(filter1);
            await _context.ParkingSpaces.DeleteOneAsync(filter2);

            return Result.CommandSuccess("Parking Space was successfully deleted from DocumentDB");
        }
    }
}