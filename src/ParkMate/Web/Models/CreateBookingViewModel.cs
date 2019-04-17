using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;

namespace Web.Models
{
    public class CreateBookingViewModel
    {
//        private ResultViewModel<Customer> _customer;
//        private ResultViewModel<ParkingSpace> _parkingSpace;
//
//        public CreateBookingViewModel(ResultViewModel<Customer> customer, ResultViewModel<ParkingSpace> parkingSpace)
//        {
//            this._customer = customer;
//            this._parkingSpace = parkingSpace;
//        }
        
        public ResultViewModel<Customer> Customer { get; set; }
        public ResultViewModel<ParkingSpace> ParkingSpace { get; set; }
    }
}
