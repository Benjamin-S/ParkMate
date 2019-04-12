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
using ParkMate.Web.Enums;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class MyParkingSpacesController : Controller
    {
        private IMediator _mediator;
        private string _userId; 

        public MyParkingSpacesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<IActionResult> Index(bool previousCommandPresent, 
            bool previousCommandResult, string previousCommandMessage)
        {
            var viewModel = new MyParkingSpacesViewModel();
            var query = new GetCustomerQuery(_userId);
            
            viewModel.QueryResult = await _mediator.Send(query);

            if (previousCommandPresent)
            {
                viewModel.PreviousCommandStatus = previousCommandResult ? CommandStatus.Success : CommandStatus.Failure;
            }
            else
            {
                viewModel.PreviousCommandStatus = CommandStatus.NoCommand;
            }
            
            viewModel.PreviousCommandResultMessage = previousCommandMessage;
            
            return View(viewModel);
        }
    }
}
