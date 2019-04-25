using System;
namespace ParkMate.ApplicationServices.DTOs
{
    public class VehicleViewModel
    {
        public int VehicleId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string Registration { get; set; }
    }
}
