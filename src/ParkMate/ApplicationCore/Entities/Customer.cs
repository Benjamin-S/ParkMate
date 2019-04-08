using System;
using System.Collections.Generic;

namespace ParkMate.ApplicationCore.Entities
{
    public class Customer
    {
        private Customer()
        {
        }

        public Customer(string identityId, string phoneNumber, string email)
        {
            IdentityId = !string.IsNullOrWhiteSpace(identityId) ?
                identityId : throw new ArgumentNullException(nameof(identityId));
            PhoneNumber = !string.IsNullOrWhiteSpace(phoneNumber) ?
                phoneNumber : throw new ArgumentNullException(nameof(phoneNumber));
            Email = !string.IsNullOrWhiteSpace(email) ?
                email : throw new ArgumentNullException(nameof(email));
        }

        public string IdentityId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public List<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();
        public List<ParkingSpace> ParkingSpaces { get; private set; } = new List<ParkingSpace>();
        public List<Booking> Bookings { get; private set; } = new List<Booking>();
    }
}
