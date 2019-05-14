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
using ParkMate.Web.Controllers;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBooking([FromForm] CreateBookingViewModel model)
        {
            model.CustomerId = _userId;

            var timeLapse = model.BookingPeriod.End - model.BookingPeriod.Start;

            Result result;
            if (timeLapse.TotalHours >= 24)
            {
                var command = new CreateDailyBookingCommand(model.CustomerId, model.VehicleId, model.ParkingSpaceId,
                    model.BookingPeriod);
                result = await _mediator.Send(command);
            }
            else
            {
                var command = new CreateHourlyBookingCommand(model.CustomerId, model.VehicleId, model.ParkingSpaceId,
                    model.BookingPeriod);
                result = await _mediator.Send(command);
            }

            return await Index(result);
        }
    }
}