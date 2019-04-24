using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ParkMate.ApplicationServices.DTOs
{
    public class ParkingSpaceViewModel
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public int ParkingSpaceId { get; set; }
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal DailyRate { get; set; }
        public bool IsVisible { get; set; }
        public int DistanceInMeters { get; set; }
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
        public AvailableTimeDTO Monday { get; set; }
        public AvailableTimeDTO Tuesday { get; set; }
        public AvailableTimeDTO Wednesday { get; set; }
        public AvailableTimeDTO Thursday { get; set; }
        public AvailableTimeDTO Friday { get; set; }
        public AvailableTimeDTO Saturday { get; set; }
        public AvailableTimeDTO Sunday { get; set; }
        public List<BookingViewModel> Bookings { get; set; } 
            = new List<BookingViewModel>();
    }
}
