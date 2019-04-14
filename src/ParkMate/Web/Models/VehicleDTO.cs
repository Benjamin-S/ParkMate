using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class VehicleDTO
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Make")]
        public string Make { get; set; }
        
        [Required]
        [StringLength(255)]
        [Display(Name = "Model")]
        public string Model { get; set; }
        
        [Required]
        [StringLength(255)]
        [Display(Name = "Colour")]
        public string Color { get; set; }
        
        [Required]
        [StringLength(255)]
        [Display(Name = "Registration Number")]
        public string Registration { get; set; }
    }
}
