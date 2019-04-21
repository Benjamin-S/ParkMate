using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.Web.Models
{
    public class CreateParkingSpaceViewModel
    {
        public ParkingSpaceDTO ParkingSpace { get; set; }
        
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }
    }
}