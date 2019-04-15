using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkMate.ApplicationCore.Entities
{
    public class Customer : BaseEntity
    {
        private Customer()
        {
        }

        public Customer(string identityId, string email)
        {
            IdentityId = !string.IsNullOrWhiteSpace(identityId) ?
                identityId : throw new ArgumentNullException(nameof(identityId));

            Email = !string.IsNullOrWhiteSpace(email) ?
                email : throw new ArgumentNullException(nameof(email));
        }

        public string IdentityId { get; private set; }
        public string PhoneNumber { get; private set; } = "";
        public string Email { get; private set; }
        public List<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();
        public List<ParkingSpace> ParkingSpaces { get; private set; } = new List<ParkingSpace>();
        public Schedule Bookings { get; private set; }
        public BookingHistory BookingHistory { get; private set; }

        public void AddBooking(Booking booking)
        {
            Bookings.AddBooking(booking);
        }
        
        public void ProcessLapsedBookings()
        {
            var lapsed = Bookings.RemoveLapsedBookings();
            lapsed.ForEach(booking => BookingHistory.AddToHistory(booking));
        }
    }
}
