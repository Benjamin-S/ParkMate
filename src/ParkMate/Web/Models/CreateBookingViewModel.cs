using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.Web.Models
{
    public class CreateBookingViewModel
    {
        public int VehicleId { get; set; }
        public int ParkingSpaceId { get; set; }
        public string CustomerId { get; set; }
        public BookingPeriodDTO BookingPeriod { get; set; }
    }
}