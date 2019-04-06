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

namespace ParkMate.ApplicationServices.Commands
{
    public class EditParkingSpaceBookingRateCommand  : IRequest<CommandResult>
    {
        public EditParkingSpaceBookingRateCommand(int parkingSpaceId, BookingRate bookingRate)
        {
            ParkingSpaceId = parkingSpaceId;
            BookingRate = bookingRate;
        }
        public int ParkingSpaceId { get; }
        public BookingRate BookingRate { get; }
    }
    
    public class EditParkingSpaceBookingRateCommandHandler 
        : IRequestHandler<EditParkingSpaceBookingRateCommand, CommandResult>
    {
        private IRepository<ParkingSpace> _repository;
        private IMediator _mediator;

        public EditParkingSpaceBookingRateCommandHandler(
            IRepository<ParkingSpace> repository,
            IMediator mediator)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
            _mediator = mediator;
        }

        public async Task<CommandResult> Handle(
            EditParkingSpaceBookingRateCommand command, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var parkingSpace = await _repository.GetByIdAsync(command.ParkingSpaceId);

            parkingSpace.UpdateBookingRate(command.BookingRate);
            
            _repository.Update(parkingSpace);
            await _repository.UnitOfWork.SaveEntitiesAsync();

            await _mediator.Publish(new ParkingSpaceRegisteredEvent(parkingSpace));

            return new CommandResult(true, "Parking Space booking rate was successfully updated");
        }
    }
}