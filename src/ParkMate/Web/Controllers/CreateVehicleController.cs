using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Commands;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class CreateVehicleController : Controller
    {
        IMediator _mediator;

        public CreateVehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] VehicleDTO dto)
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var vehicle = new Vehicle(dto.Make, dto.Model, dto.Color, dto.Registration);
            var command = new AddNewVehicleCommand(customerId, vehicle);
            var result = await _mediator.Send(command);
            return View(result);
        }
    }
}
