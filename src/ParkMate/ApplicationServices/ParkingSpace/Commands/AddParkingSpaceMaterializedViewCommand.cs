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
    public class AddParkingSpaceMaterializedViewCommand : IRequest<Result>
    {
        public AddParkingSpaceMaterializedViewCommand(
            ParkingSpace parkingSpace)
        {
            ParkingSpace = parkingSpace;
        }
        public ParkingSpace ParkingSpace { get; }
    }

    public class AddParkingSpaceMaterializedViewCommandHandler
        : IRequestHandler<AddParkingSpaceMaterializedViewCommand, Result>
    {
        private IMongoContext _context;
        private IMapper _mapper;

        public AddParkingSpaceMaterializedViewCommandHandler(
            IMongoContext context,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(
            AddParkingSpaceMaterializedViewCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var space = _mapper.Map<ParkingSpace, ParkingSpaceViewModel>(command.ParkingSpace);

            await _context.ParkingSpaces.InsertOneAsync(space);

            return Result.Ok();
        }
    }
}