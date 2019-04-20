using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationServices.Queries;
using Web.Models;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Commands;

namespace Web.Controllers
{
    [Authorize]
    public class CreateBookingController : Controller
    {

        private IMediator _mediator;
        private readonly string _userId;


        public CreateBookingController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<IActionResult> Index(Result previousCommand, int id)
        {
            var customerQuery = new GetCustomerQuery(_userId);
            var parkingSpaceQuery = new GetSingleParkingSpaceQuery(id);
            var viewModel = new CreateBookingViewModel
            {
                Customer = new ResultViewModel<Customer>()
                {
                    Command = previousCommand,
                    Query = await _mediator.Send(customerQuery)
                },

                ParkingSpace = new ResultViewModel<ParkingSpace>
                {
                    Command = previousCommand,
                    Query = await _mediator.Send(parkingSpaceQuery)
                }
            };

            return View("Index", viewModel);
        }

        public IActionResult EditBooking()
        {
            return View();
        }

        public IActionResult RemoveBooking()
        {
            return View();
        }

        public IActionResult CreateBooking()
        {
            return View();
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> CreateBooking([FromForm] CreateBookingViewModel model)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View(model);
        //     }

        //     // var result = await _mediator.Send(new BookParkingSpaceCommand(_userId, ));
        //     var query = new GetCustomerQuery(_userId);
        //     var result = await _mediator.Send(query);
        //     return await Index(result);
        // }
    }
}
