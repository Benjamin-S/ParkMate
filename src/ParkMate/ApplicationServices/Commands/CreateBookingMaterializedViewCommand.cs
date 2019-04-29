
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
    public class CreateBookingMaterializedViewCommand : IRequest<Result>
    {
        public CreateBookingMaterializedViewCommand(
            Booking booking)
        {
            Booking = booking;
        }
        public Booking Booking { get; }
    }

    public class CreateBookingMaterializedViewCommandHandler
        : IRequestHandler<CreateBookingMaterializedViewCommand, Result>
    {
        private IMongoContext _context;
        private IMapper _mapper;

        public CreateBookingMaterializedViewCommandHandler(
            IMongoContext context,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(
            CreateBookingMaterializedViewCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var booking = _mapper.Map<Booking, BookingViewModel>(command.Booking);

            await _context.Bookings.InsertOneAsync(booking);

            return Result.Ok();
        }
    }
}