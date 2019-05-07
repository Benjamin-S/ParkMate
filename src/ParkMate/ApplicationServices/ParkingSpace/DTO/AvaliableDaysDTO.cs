using System.ComponentModel.DataAnnotations;

namespace ParkMate.ApplicationServices.DTOs
{
    public class AvailableDaysDTO
    {
        [Required]
        [Display(Name = "Monday")]
        public bool IsAvailableMonday { get; set; } = true;

        [Required]
        [Display(Name = "Tuesday")]
        public bool IsAvailableTuesday { get; set; }

        [Required]
        [Display(Name = "Wednesday")]
        public bool IsAvailableWednesday { get; set; }

        [Required]
        [Display(Name = "Thursday")]
        public bool IsAvailableThursday { get; set; }

        [Required]
        [Display(Name = "Friday")]
        public bool IsAvailableFriday { get; set; }

        [Required]
        [Display(Name = "Saturday")]
        public bool IsAvailableSaturday { get; set; }

        [Required]
        [Display(Name = "Sunday")]
        public bool IsAvailableSunday { get; set; }
    }
}