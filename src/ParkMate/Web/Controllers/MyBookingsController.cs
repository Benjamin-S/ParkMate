using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationServices.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Queries;
using ParkMate.ApplicationServices.Commands;
using ParkMate.Web.Models;

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

        public IActionResult EditBooking()
        {
            return View();
        }

        public async Task<IActionResult> CancelBooking(int id)
        {
            var query = new GetBookingQuery(id);
            var viewModel = new ResultViewModel<BookingViewModel>
            {
                Query = await _mediator.Send(query)
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("CancelBooking")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBookingConfirm(int id)
        {
            var command = new CancelBookingCommand(id, _userId);
            var result = await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ViewBooking(Result previousCommand, int id)
        {
            var query = new GetBookingQuery(id);

            var viewModel = new ResultViewModel<BookingViewModel>()
            {
                Command = previousCommand,
                Query = await _mediator.Send(query)
            };
            return View(viewModel);
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
            
            return View(viewModel);
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
                    break;
                }
                case 2:
                {
                    model.Booking.Rate = model.DailyRate;
                    var command = new CreateDailyBookingCommand(_userId, model.VehicleId, model.ParkingSpaceId, model.Booking);
                    result = await _mediator.Send(command);
                    break;
                }
                default:
                    return View(model);
            }
            return await Index(result);
        }
    }
}