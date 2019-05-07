using MediatR;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Events
{
    public class NewBookingCreatedEvent : INotification
    {
        public NewBookingCreatedEvent(Customer buyer, Customer seller, Booking booking)
        {
            Buyer = buyer;
            Seller = seller;
            Booking = booking;
        }

        public Customer Buyer { get; }
        public Customer Seller { get; }
        public Booking Booking { get; }
    }
}