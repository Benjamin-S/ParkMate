using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationServices.Queries;
using Web.Models;

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

        // GET/id
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var customerQuery = new GetCustomerQuery(_userId);
            var parkingSpaceQuery = new GetSingleParkingSpaceQuery(id);
            var viewModel = new CreateBookingViewModel
            {
                Customer = await _mediator.Send(customerQuery),
                ParkingSpace = await _mediator.Send(parkingSpaceQuery)
            };

            return View(viewModel);
        }
    }
}
