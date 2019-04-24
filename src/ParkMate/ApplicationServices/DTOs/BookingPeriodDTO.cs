using System;

namespace ParkMate.ApplicationServices.DTOs
{
    public class BookingPeriodDTO
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
        public decimal Rate { get; set; }
    }
}
