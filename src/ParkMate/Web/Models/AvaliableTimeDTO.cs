using System;
using System.ComponentModel.DataAnnotations;

namespace ParkMate.Web.Models
{
    public class AvailableTimeDTO
    {
        [Required]
        [Display(Name = "Availability Start Time")]
        public TimeSpan AvailableFrom { get; set; }
        [Required]
        [Display(Name = "Availability End Time")]
        public TimeSpan AvailableTo { get; set; }
    }
}