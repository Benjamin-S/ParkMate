using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkMate.ApplicationServices.DTOs
{
    public class BookingViewModel
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string CustomerId { get; set; }
        public ParkingSpaceViewModel ParkingSpace { get; set; }
        public VehicleViewModel Vehicle { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal Total { get; set; }
        public decimal Rate { get; set; }
        public string BillingUnit { get; set; }
        public int BookingUnits { get; set; }

        public DateTime BookingTime { get; set; } 
    }
}
