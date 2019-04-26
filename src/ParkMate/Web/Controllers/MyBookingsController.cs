using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Queries;
using ParkMate.ApplicationServices.Commands;
using ParkMate.Web.Models;

//It would be more consistent if we had CreateBooking,
//EditBooking as seperate views controlled by the MyBookings
//controller. This also allows for the proper display of Result
//objects returned from commands/queries. So for instance the
//EditBooking function on the MyBookings controller should create
//a new command, pass it to the mediator, then attach the result
//to a ResultViewModel and pass that to the MyBookings Index view,
//similarly to any of the Edit functions on MyParkingSpacesController

namespace Web.Controllers
{
    [Authorize]
    public class MyBookingsController : Controller
    {
        private IMediator _mediator;
        private string _userId;

        public MyBookingsController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<IActionResult> Index(Result previousCommand)
        {
            var historicalQuery = new GetHistoricalBookingsForCustomerQuery(_userId);
            var futureQuery = new GetFutureBookingsForCustomerQuery(_userId);
            var viewModel = new MyBookingsViewModel
            {
                HistoricalBookings = new ResultViewModel<IReadOnlyList<BookingViewModel>>
                {
                    Command = previousCommand,
                    Query = await _mediator.Send(historicalQuery)
                },
                
                FutureBookings = new ResultViewModel<IReadOnlyList<BookingViewModel>>
                {
                    Command = previousCommand,
                    Query = await _mediator.Send(futureQuery)
                }             
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> CreateBooking(Result previousCommand, int id)
        {
            var customerQuery = new GetCustomerQuery(_userId);
            var parkingSpaceQuery = new GetSingleParkingSpaceQuery(id);
            var viewModel = new CreateBookingViewModel
            {
                Customer = new ResultViewModel<CustomerViewModel>
                {
                    Command = previousCommand,
                    Query = await _mediator.Send(customerQuery)
                },

                ParkingSpace = new ResultViewModel<ParkingSpaceViewModel>
                {
                    Command = previousCommand,
                    Query = await _mediator.Send(parkingSpaceQuery)
                }
            };

            return View("CreateBooking", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] CreateBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Result result;
            switch (model.BookingType)
            {
                case 1:
                {
                    model.Booking.Rate = model.HourlyRate;
                    var command = new CreateHourlyBookingCommand(_userId, model.VehicleId, model.ParkingSpaceId, model.Booking);
                    result = await _mediator.Send(command);
                    return await Index(result);
                }
                case 2:
                {
                    model.Booking.Rate = model.DailyRate;
                    var command = new CreateDailyBookingCommand(_userId, model.VehicleId, model.ParkingSpaceId, model.Booking);
                    result = await _mediator.Send(command);
                    return await Index(result);
                }
                default:
                    return View(model);
            }
        }
    }
}
