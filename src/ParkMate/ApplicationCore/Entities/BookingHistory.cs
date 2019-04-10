using System;
using System.Collections.Generic;
using System.Linq;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationCore.Entities
{
    public class BookingHistory : BaseEntity
    {
        public BookingHistory()
        {
        }

        private List<Booking> _bookings = new List<Booking>();
        public IEnumerable<Booking> Bookings
        {
            get => _bookings.AsEnumerable();
            private set => _bookings = (List<Booking>)value;
        }

        internal void AddToHistory(Booking booking)
        {
            _bookings.Add(booking);
        }
    }
}
