using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;

namespace ParkMate.ApplicationServices.DTOs
{
    public class ParkingSpaceListingDTO
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal DailyRate { get; set; }
        public int ParkingSpaceId  { get; set; }
        public bool IsListed { get; set; }
        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
    }
}