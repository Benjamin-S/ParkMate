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
            double latitude,
            double longitude)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            Zip = zip ?? throw new ArgumentNullException(nameof(zip));
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Zip;
            yield return Latitude;
            yield return Longitude; 
        }
    }
}