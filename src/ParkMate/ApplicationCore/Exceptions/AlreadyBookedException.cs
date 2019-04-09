using System;
using ParkMate.ApplicationCore.ValueObjects;

namespace ParkMate.ApplicationCore.Exceptions
{
    public class AlreadyBookedException : Exception
    {
        public AlreadyBookedException(BookingPeriod booking) 
            : base($"Booking from {booking.Start} - {booking.End} overlaps with an existing booking")
        {
        }
    }
}
