using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkMate.ApplicationServices.DTOs
{
    public class CustomerViewModel
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        public string Name { get; set; }
        public List<VehicleViewModel> Vehicles { get; set; } = new List<VehicleViewModel>();
        public List<ParkingSpaceViewModel> ParkingSpaces { get; set; } = new List<ParkingSpaceViewModel>();
    }
}
