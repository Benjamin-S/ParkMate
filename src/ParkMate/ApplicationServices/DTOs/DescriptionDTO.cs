using System.ComponentModel.DataAnnotations;

namespace ParkMate.ApplicationServices.DTOs
{
    public class DescriptionDTO
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Parking Space Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Parking Space Description")]
        public string Description { get; set; }

        public string ImageURL { get; set; }
    }
}