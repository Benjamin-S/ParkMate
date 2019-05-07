using System;
using System.ComponentModel.DataAnnotations;

namespace ParkMate.ApplicationServices.DTOs
{
    public class AvailableTimeDTO
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Availability Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan AvailableFrom { get; set; }
        
        [Required]
        [Display(Name = "Availability End Time")]
        [DataType(DataType.Time)]
        public TimeSpan AvailableTo { get; set; }
    }
}