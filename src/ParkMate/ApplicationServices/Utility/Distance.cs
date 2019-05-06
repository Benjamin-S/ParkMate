using System;
using ParkMate.ApplicationCore.ValueObjects;

namespace ParkMate.ApplicationServices.Util
{
    public class Distance
    {
        public static double Haversine(Point location1, Point location2)
        {
            var lat1 = location1.Latitude;
            var lat2 = location2.Latitude;
            var lon1 = location1.Longitude;
            var lon2 = location2.Longitude;

            var R = 6372.8; // In kilometers
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            var a = Math.Sin(dLat / 2) 
                * Math.Sin(dLat / 2) 
                + Math.Sin(dLon / 2) 
                * Math.Sin(dLon / 2) 
                * Math.Cos(lat1) 
                * Math.Cos(lat2);

            return R * 2 * Math.Asin(Math.Sqrt(a)) * 1000; //meters
        }

        public static double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
