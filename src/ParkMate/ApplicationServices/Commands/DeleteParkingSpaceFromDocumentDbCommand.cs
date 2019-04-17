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

        public DeleteParkingSpaceFromDocumentDbCommandHandler(
            IMongoContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> Handle(
            DeleteParkingSpaceFromDocumentDbCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.ParkingSpaceListings.DeleteOneAsync(ps => 
                        ps.ParkingSpaceId == command.ParkingSpace.Id);

            await _context.ParkingSpaces.DeleteOneAsync(
                        ps => ps.Id == command.ParkingSpace.Id);

            return Result.Ok();
        }
    }
}