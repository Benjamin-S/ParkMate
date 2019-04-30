using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.Web.Models
{
    public class CreateBookingViewModel
    {
        public int VehicleId { get; set; }
        public int ParkingSpaceId { get; set; }
        public string CustomerId { get; set; }
        public int BookingType { get; set; }
        public decimal DailyRate { get; set; }
        public decimal HourlyRate { get; set; }
        public BookingPeriodDTO Booking { get; set; }
        public ResultViewModel<CustomerViewModel> Customer { get; set; }
        public ResultViewModel<ParkingSpaceViewModel> ParkingSpace { get; set; }
    }
}