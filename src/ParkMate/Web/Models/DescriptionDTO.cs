using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ParkMate.Web.Models
{
    public class DescriptionDTO
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Parking Space Title")]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        [Display(Name = "Parking Space Description")]
        public string Description { get; set; }
        public string ImageURL { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}