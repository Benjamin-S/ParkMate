using System.ComponentModel.DataAnnotations;

namespace ParkMate.Web.Models
{
    public class BookingRateDTO
    {
        [Required]
        [DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal DailyRate { get; set; }
    }
}