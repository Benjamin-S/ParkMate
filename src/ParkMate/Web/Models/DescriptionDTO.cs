using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ParkMate.Web.Models
{
    public class DescriptionDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}