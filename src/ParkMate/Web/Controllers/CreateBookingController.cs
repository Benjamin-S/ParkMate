using System.Security.Claims;
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
        
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        // GET/id
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var query = new GetCustomerQuery(_userId);
            var result = await _mediator.Send(query);
            return View(result);
        }
    }
}