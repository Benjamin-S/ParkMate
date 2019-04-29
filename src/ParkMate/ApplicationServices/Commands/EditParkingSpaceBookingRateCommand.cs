using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Interfaces;
using ParkMate.ApplicationServices.Events;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Commands
{
    public class EditParkingSpaceBookingRateCommand  : IRequest<Result>
    {
        public EditParkingSpaceBookingRateCommand(
            int parkingSpaceId, 
            string ownerId, 
            BookingRateDTO bookingRate)
        {
            ParkingSpaceId = parkingSpaceId;
            OwnerId = ownerId;
            BookingRate = bookingRate;
        }
        public int ParkingSpaceId { get; }
        public string OwnerId { get; }
        public BookingRateDTO BookingRate { get; }
    }
    
    public class EditParkingSpaceBookingRateCommandHandler 
        : IRequestHandler<EditParkingSpaceBookingRateCommand, Result>
    {
        private IParkingSpaceRepository _repository;
        private IMediator _mediator;

        public EditParkingSpaceBookingRateCommandHandler(
            IParkingSpaceRepository repository,
            IMediator mediator)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<Result> Handle(
            EditParkingSpaceBookingRateCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            if (!parkingSpace.OwnerId.Equals(command.OwnerId))
            {
                return Result.CommandFail("Not authorized to modify this Parking Space");
            }

            var rate = new BookingRate(
                new Money(command.BookingRate.HourlyRate), 
                new Money(command.BookingRate.DailyRate));

            parkingSpace.UpdateBookingRate(rate);
            
            _repository.Update(parkingSpace);

            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceUpdatedEvent(parkingSpace));

            return Result.CommandSuccess("Parking Space booking rate was successfully updated");
        }
    }
}