using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Queries;
using ParkMate.Web.Enums;

namespace Web.Models
{
    public class MyParkingSpacesViewModel
    {
        public string PreviousCommandResultMessage { get; set; }
        public CommandStatus PreviousCommandStatus { get; set; } = CommandStatus.NoCommand;
        public QueryResult<Customer> QueryResult { get; set; }
    }
}