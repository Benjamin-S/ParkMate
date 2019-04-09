using System.ComponentModel.DataAnnotations;

namespace ParkMate.Web.Models
{
    public class AddressDTO
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Street Address")]
        public string Street { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Range(1000,9999)]
        [Display(Name = "Postcode")]
        public string Zip { get; set; }

        [Required]
        [Range(-180.999999999, 180.999999999)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180.999999999, 180.999999999)]
        public double Longitude { get; set; }

    }
}