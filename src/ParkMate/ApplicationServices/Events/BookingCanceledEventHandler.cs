using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkMate.ApplicationServices.Commands;

namespace ParkMate.ApplicationServices.Events
{
    public class BookingCanceledEventHandler :
        INotificationHandler<BookingCanceledEvent>
    {
        private IMediator _mediator;

        public BookingCanceledEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(
            BookingCanceledEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new ReplaceBookingMaterializedViewCommand(notification.Booking);
            await _mediator.Send(command);
        }
    }
}