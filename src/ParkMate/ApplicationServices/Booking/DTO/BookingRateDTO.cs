using System.ComponentModel.DataAnnotations;

namespace ParkMate.ApplicationServices.DTOs
{
    public class BookingRateDTO
    {
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Daily Rate")]
        public decimal DailyRate { get; set; }
    }
}