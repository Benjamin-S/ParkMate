using System;
namespace ParkMate.ApplicationServices.DTOs
{
    public class AvailabilityDTO
    {
        public bool IsVisible { get; set; }
        public AvailableTimeDTO Monday { get; set; }
        public AvailableTimeDTO Tuesday { get; set; }
        public AvailableTimeDTO Wednesday { get; set; }
        public AvailableTimeDTO Thursday { get; set; }
        public AvailableTimeDTO Friday { get; set; }
        public AvailableTimeDTO Saturday { get; set; }
        public AvailableTimeDTO Sunday { get; set; }
    }
}
