using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;

namespace Web.Models
{
    public class BookingViewModel
    {
        public QueryResult<ParkingSpace> SpaceQueryResult { get; set; }
        public QueryResult<Customer> CustomerQueryResult { get; set; }
    }
}