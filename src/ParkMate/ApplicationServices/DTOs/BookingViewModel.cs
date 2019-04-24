using System;

namespace ParkMate.ApplicationServices.DTOs
{
    public class BookingViewModel
    {
        public string CustomerId { get; set; }
        public ParkingSpaceViewModel ParkingSpace { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal Total { get; set; }
        public decimal Rate { get; set; }
        public string BillingUnit { get; set; }
        public int BookingUnits { get; set; }

        public DateTime BookingTime { get; set; } 
    }
}
