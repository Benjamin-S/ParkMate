using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Enums;
using MediatR;
using ParkMate.ApplicationCore.Interfaces;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.ApplicationServices.Events
{
    public class NewBookingSellerEmailHandler :
        INotificationHandler<NewBookingCreatedEvent>
    {
        private IEmailService _emailSender;

        public NewBookingSellerEmailHandler(IEmailService emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Handle(
            NewBookingCreatedEvent notification,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var title = notification.Booking.ParkingSpace.Description.Title;
            var booking = notification.Booking.BookingInfo;
            string units = notification.Booking.BookingInfo.BillingUnit == BillingUnit.Hourly ? "Hours" : "Days";

            await _emailSender.SendEmailAsync(notification.Seller.Email,
                $"New booking for {title} on {notification.Booking.BookingInfo.Start} received",
                $"Dear {notification.Buyer.Name}," +
                $"<br/><br/>Congratulations! You have received a new booking for {title}. " +
                    "Please see below the details of the booking:<br/>" +
                    "<br/>&nbsp;Booking Reference: " + notification.Booking.Id +
                    "<br/>&nbsp;ParkingSpace Name: " + title +
                    "<br/>&nbsp;Description: " + notification.Booking.ParkingSpace.Description.Description +
                    "<br/>&nbsp;Time of Booking: " + booking.Start.ToString("dddd, dd MMMM yyyy") +
                    "<br/>&nbsp;Owner: " + notification.Seller.Name +
                    "<br/>&nbsp;Contact Info: " + notification.Seller.Email +
                    "<br/>&nbsp;Address " + notification.Booking.ParkingSpace.Address.Street +
                    "<br/>&nbsp;Amount Paid:" +
                    "<br/>&nbsp;&nbsp;Transaction ID: " + notification.Booking.Id +
                    "<br/>&nbsp;&nbsp;Parking Space Rate: " + booking.Rate +
                    $"<br/><br/>&nbsp;&nbsp; {units} Booked: " + booking.BookingUnits + " " + units +
                    "<br/>&nbsp;&nbsp; Subtotal: " + booking.Total +
                    "<br/>&nbsp;&nbsp; GST: " + Money.ValueAsString(booking.Total.Value * 0.1m) +
                    "<br/>&nbsp;&nbsp; -------------------------------" +
                    "<br/>&nbsp;&nbsp; Total: " + Money.ValueAsString(booking.Total.Value + booking.Total.Value * 0.1m) +
                "<br/><br/>" +
                "To manage your bookings head, to your profile page." +
                "<br/><br/>" +
                "Sincerely,<br/>" +
                "The Parkmate Team");
        }
    }
}