using System;
using System.Collections.Generic;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class Address : ValueObject
    {
        private Address()
        {
        }

        public Address(
            string street,
            string city,
            string state,
            string zip,
            Point location)
        {
            Street = !string.IsNullOrWhiteSpace(street) ? 
                street : throw new ArgumentNullException(nameof(street));
            City = !string.IsNullOrWhiteSpace(city) ? 
                city : throw new ArgumentNullException(nameof(city));
            State = !string.IsNullOrWhiteSpace(state) ? 
                state : throw new ArgumentNullException(nameof(state));
            Zip = !string.IsNullOrWhiteSpace(zip) ? 
                zip : throw new ArgumentNullException(nameof(zip));
            Location = location ?? 
                throw new ArgumentNullException(nameof(location));
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
        public Point Location { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Zip;
            yield return Location;
        }
    }
}