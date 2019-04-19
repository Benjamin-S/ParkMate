using System;

namespace ParkMate.ApplicationServices.DTOs
{
    public class ParkingSpaceDTO
    {
        public string OwnerId { get; set; }
        public AddressDTO Address { get; set;}
        public BookingRateDTO BookingRate { get; set; }
        public DescriptionDTO Description { get; set; }
    } 
}