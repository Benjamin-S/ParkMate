using System.Collections.Generic;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.Web.Models
{
    public class MyBookingsViewModel
    {
        public ResultViewModel<IReadOnlyList<BookingViewModel>> HistoricalBookings { get; set; }
        public ResultViewModel<IReadOnlyList<BookingViewModel>> FutureBookings { get; set; }
    }
}