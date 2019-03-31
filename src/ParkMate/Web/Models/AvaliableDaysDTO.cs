using System.ComponentModel.DataAnnotations;

namespace ParkMate.Web.Models
{
    public class AvailableDaysDTO
    {
        [Required]
        public bool IsAvailableMonday { get; set; } = true;
        [Required]
        public bool IsAvailableTuesday { get; set; }
        [Required]
        public bool IsAvailableWednesday { get; set; }
        [Required]
        public bool IsAvailableThursday { get; set; }
        [Required]
        public bool IsAvailableFriday { get; set; }
        [Required]
        public bool IsAvailableSaturday { get; set; }
        [Required]
        public bool IsAvailableSunday { get; set; }
    }
}