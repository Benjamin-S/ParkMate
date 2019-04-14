using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.Queries;

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

        public async Task<IActionResult> Index()
        {
            var query = new GetCustomerQuery(_userId);
            var result = await _mediator.Send(query);
            return View(result);
        }
    }
}
