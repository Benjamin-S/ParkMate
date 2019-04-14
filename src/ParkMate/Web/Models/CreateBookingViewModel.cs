using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;

namespace Web.Models
{
    public class CreateBookingViewModel
    {
        public QueryResult<Customer> Customer { get; set; }
        public QueryResult<ParkingSpace> ParkingSpace { get; set; }
    }
}
