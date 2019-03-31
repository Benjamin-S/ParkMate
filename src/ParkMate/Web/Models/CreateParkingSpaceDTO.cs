using System;
using Microsoft.AspNetCore.Http;

namespace ParkMate.Web.Models
{
    public class CreateParkingSpaceDTO
    {
        public AddressDTO Address { get; set;}
        public BookingRateDTO BookingRate { get; set; }
        public DescriptionDTO Description { get; set; }
        public AvailableTimeDTO AvailableTime { get; set; }
        public AvailableDaysDTO AvailableDays { get; set; }
    } 
}