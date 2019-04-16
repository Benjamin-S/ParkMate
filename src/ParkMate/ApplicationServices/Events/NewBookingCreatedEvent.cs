using MediatR;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Events
{
    public class NewBookingCreatedEvent : INotification
    {
        public NewBookingCreatedEvent(Customer customer, ParkingSpace parkingSpace)
        {
            Customer = customer;
            ParkingSpace = parkingSpace;
        }

        public Customer Customer { get; }
        public ParkingSpace ParkingSpace { get; }
    }
}