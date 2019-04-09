using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.Queries;

namespace Web.Controllers
{
    public class MyBookingsController : Controller
    {
        private IMediator _mediator;
        private string _userId; 

        public MyBookingsController(IMediator mediator)
        {
            _mediator = mediator;
            _userId = "test"; //User.FindFirst(ClaimTypes.NameIdentifier).ToString();
        }

        public async Task<IActionResult> Index()
        {
            // var query = new GetAllBookingsForOwnerQuery(_userId);
            // var result = await _mediator.Send(query);
            // return View(result);
            return View();
        }
    }
}
