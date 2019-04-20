using System;
namespace ParkMate.ApplicationServices.DTOs
{
    public class DistanceSearchDTO
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int DistanceInMeters { get; set; }
    }
}
