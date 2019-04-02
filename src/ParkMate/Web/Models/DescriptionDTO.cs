using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

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
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }
    }
}