using System;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using AutoMapper;
using MongoDB.Driver;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class ReplaceBookingMaterializedViewCommand : IRequest<Result>
    {
        public ReplaceBookingMaterializedViewCommand(Booking booking)
        {
            Booking = booking;
        }
        public Booking Booking { get; }
    }

    public class ReplaceBookingMaterializedViewCommandHandler
        : IRequestHandler<ReplaceBookingMaterializedViewCommand, Result>
    {
        private IMongoContext _context;
        private IMapper _mapper;

        public ReplaceBookingMaterializedViewCommandHandler(
            IMongoContext context,
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result> Handle(
            ReplaceBookingMaterializedViewCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var booking = _mapper.Map<Booking, BookingViewModel>(command.Booking);

            await _context.Bookings.ReplaceOneAsync(c =>
                c.BookingId.Equals(command.Booking.Id),
                booking,
                new UpdateOptions { IsUpsert = true });

            return Result.Ok();
        }
    }
}