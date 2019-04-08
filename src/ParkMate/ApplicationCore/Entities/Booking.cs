using System;
using ParkMate.ApplicationCore.ValueObjects;

namespace ParkMate.ApplicationCore.Entities
{
    public class Booking : BaseEntity
    {
        private Booking()
        {
        }
        
        public Booking(ParkingSpace parkingSpace, Vehicle vehicle, BookingPeriod bookingPeriod)
        {
            ParkingSpace = parkingSpace ?? 
                throw new ArgumentNullException(nameof(parkingSpace));
            Vehicle = vehicle ?? 
                throw new ArgumentNullException(nameof(vehicle));
            BookingPeriod = bookingPeriod ?? 
                throw new ArgumentNullException(nameof(bookingPeriod));
        }

        public ParkingSpace ParkingSpace { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public BookingPeriod BookingPeriod { get; private set; }
    }
}
