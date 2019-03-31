using System.ComponentModel.DataAnnotations;

namespace ParkMate.Web.Models
{
    public class AddressDTO
    {
        [Required]
        [StringLength(50)]
        public string Street { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        public string State { get; set; }
        [Required]
        [Range(1000,9999)]
        public string Zip { get; set; }
        [Required]
        [Range(-180.999999999, 180.999999999)]
        public double Latitude { get; set; }
        [Required]
        [Range(-180.999999999, 180.999999999)]
        public double Longitude { get; set; }
    }
}