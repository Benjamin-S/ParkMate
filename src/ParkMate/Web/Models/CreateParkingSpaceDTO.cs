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
    public class AddressDTO
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class BookingRateDTO
    {
        public decimal HourlyRate { get; set; }
        public decimal DailyRate { get; set; }
    }
    public class DescriptionDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public IFormFile ImageFile { get; set; }
    }
    public class AvailableTimeDTO
    {
        public TimeSpan AvailableFrom { get; set; }
        public TimeSpan AvailableTo { get; set; }
    }
    public class AvailableDaysDTO
    {
        public bool IsAvailableMonday { get; set; } = true;
        public bool IsAvailableTuesday { get; set; }
        public bool IsAvailableWednesday { get; set; }
        public bool IsAvailableThursday { get; set; }
        public bool IsAvailableFriday { get; set; }
        public bool IsAvailableSaturday { get; set; }
        public bool IsAvailableSunday { get; set; }
    } 
}