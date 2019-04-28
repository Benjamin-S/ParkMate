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
    public class ReplaceParkingSpaceMaterializedViewCommand : IRequest<Result>
    {
        public ReplaceParkingSpaceMaterializedViewCommand(
            ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }
        public ParkingSpace ParkingSpace { get; }
    }

    public class ReplaceParkingSpaceMaterializedViewCommandHandler
        : IRequestHandler<ReplaceParkingSpaceMaterializedViewCommand, Result>
    {
        private IMongoContext _context;
        private IMapper _mapper;

        public ReplaceParkingSpaceMaterializedViewCommandHandler(
            IMongoContext context,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(
            ReplaceParkingSpaceMaterializedViewCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = _mapper.Map<ParkingSpaceViewModel>(command.ParkingSpace);

            await _context.ParkingSpaces.ReplaceOneAsync(doc => 
                doc.ParkingSpaceId == command.ParkingSpace.Id,
                parkingSpace,
                new UpdateOptions { IsUpsert = true });
        
            return Result.Ok();
        }
    }
}