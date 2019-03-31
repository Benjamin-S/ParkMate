using System;
using System.ComponentModel.DataAnnotations;

namespace ParkMate.Web.Models
{
    public class AvailableTimeDTO
    {
        [Required]
        public TimeSpan AvailableFrom { get; set; }
        [Required]
        public TimeSpan AvailableTo { get; set; }
    }
}