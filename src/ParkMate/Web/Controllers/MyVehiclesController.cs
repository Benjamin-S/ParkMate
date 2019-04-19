using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Queries;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class MyVehiclesController : Controller
    {
        private IMediator _mediator;
        private string _userId; 

        public MyVehiclesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<IActionResult> Index(Result previousCommand)
        {
            var query = new GetCustomerQuery(_userId);
            var viewModel = new ResultViewModel<Customer>
            {
                Command = previousCommand,
                Query = await _mediator.Send(query)
            };
  
            return View("Index", viewModel);
        }

        public IActionResult AddVehicle()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromForm] VehicleDTO dto)
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var command = new AddNewVehicleCommand(customerId, dto);
            var result = await _mediator.Send(command);
            return await Index(result);
        }
    }
}
