using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Queries;

namespace Web.Models
{
    public class MyParkingSpacesViewModel
    {
        public string PreviousCommandResultMessage { get; set; }
        public bool? PreviousCommandResult { get; set; }
        public QueryResult<Customer> QueryResult { get; set; }
    }
}