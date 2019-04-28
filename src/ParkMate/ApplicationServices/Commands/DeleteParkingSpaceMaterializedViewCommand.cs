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
    public class DeleteParkingSpaceMaterializedViewCommand : IRequest<Result>
    {
        public DeleteParkingSpaceMaterializedViewCommand(
            ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }
        public ParkingSpace ParkingSpace { get; }
    }

    public class DeleteParkingSpaceMaterializedViewCommandHandler
        : IRequestHandler<DeleteParkingSpaceMaterializedViewCommand, Result>
    {
        private IMongoContext _context;

        public DeleteParkingSpaceMaterializedViewCommandHandler(
            IMongoContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<Result> Handle(
            DeleteParkingSpaceMaterializedViewCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await _context.ParkingSpaces.DeleteOneAsync(
                        ps => ps.ParkingSpaceId == command.ParkingSpace.Id);

            return Result.Ok();
        }
    }
}