using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.Web.Models;

namespace ParkMate.Web.Models
{
    public class ParkingSpaceDescriptionViewModel
    {
        public DescriptionDTO Description { get; set; }
        
        [Required]
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }
    }
}