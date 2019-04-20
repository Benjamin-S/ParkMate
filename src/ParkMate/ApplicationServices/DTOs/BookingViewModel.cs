using System;
namespace ParkMate.ApplicationServices.DTOs
{
    public class BookingViewModel
    {
        public ParkingSpaceDTO ParkingSpace { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal Charge { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
