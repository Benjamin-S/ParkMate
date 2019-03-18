using System.Collections.Generic;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Zip { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public Address(
            string street,
            string city,
            string state,
            string zip,
            double latitude,
            double longitude)
        {
            Street = street;
            City = city;
            State = state; 
            Zip = zip;
            Latitude = latitude;
            Longitude = longitude;
        }
        
        
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