using System;
using System.Collections.Generic;
using System.Linq;
using ParkMate.ApplicationCore.Exceptions;
using ParkMate.ApplicationCore.Util;
using ParkMate.ApplicationCore.ValueObjects;

namespace ParkMate.ApplicationCore.Entities
{
    public class Schedule : BaseEntity
    {
        public Schedule()
        {
        }

        private List<Booking> _bookings = new List<Booking>();
        public IEnumerable<Booking> FutureBookings
        {
            get => _bookings.AsEnumerable();
            private set => _bookings = (List<Booking>)value;
        }

        internal void AddBooking(Booking booking)
        {
            if (!IsAvailable(booking.BookingPeriod))
            {
                throw new AlreadyBookedException(booking.BookingPeriod);
            }
            _bookings.Add(booking);
        }

        internal bool IsAvailable(BookingPeriod bookingPeriod)
        {
            foreach (var booking in FutureBookings)
            {
                if (booking.BookingPeriod.Overlaps(bookingPeriod))
                {
                    return false;
                }
            }
            return true;
        }

        internal List<Booking> RemoveLapsedBookings()
        {
            var lapsed = _bookings.Where(booking => booking.BookingPeriod.End < SystemTime.Now()).ToList();
            lapsed.ForEach(booking => _bookings.Remove(booking));
            return lapsed;
        }
    }
}
