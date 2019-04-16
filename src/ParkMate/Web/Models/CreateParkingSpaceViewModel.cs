using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ParkMate.Web.Models
{
    public class CreateParkingSpaceViewModel
    {
        public ParkingSpaceDTO ParkingSpace { get; set; }
        
        [Required]
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }
    }
}