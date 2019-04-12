using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ParkMate.ApplicationServices.DTOs
{
    [BsonIgnoreExtraElements]
    public class SearchAddressDTO
    {
        public int Id { get; set; }
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
