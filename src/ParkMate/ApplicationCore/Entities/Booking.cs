using System;
using ParkMate.ApplicationCore.ValueObjects;

namespace ParkMate.ApplicationCore.Entities
{
    public class Booking : BaseEntity
    {
        private Booking()
        {
        }
        
        public Booking(string customerId, ParkingSpace parkingSpace, Vehicle vehicle, BookingInfo bookingPeriod)
        {
            CustomerId = !string.IsNullOrWhiteSpace(customerId) ?
                customerId : throw new ArgumentNullException(nameof(customerId));
            ParkingSpace = parkingSpace ?? 
                throw new ArgumentNullException(nameof(parkingSpace));
            Vehicle = vehicle ?? 
                throw new ArgumentNullException(nameof(vehicle));
            BookingInfo = bookingPeriod ?? 
                throw new ArgumentNullException(nameof(bookingPeriod));
        }

        public string CustomerId { get; private set; }
        public ParkingSpace ParkingSpace { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public BookingInfo BookingInfo { get; private set; }
        public DateTime BookingTime { get; private set; }
    }
}
