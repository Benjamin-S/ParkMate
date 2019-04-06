using System;
using System.Collections.Generic;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class Point : ValueObject
    {
        public Point(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}
