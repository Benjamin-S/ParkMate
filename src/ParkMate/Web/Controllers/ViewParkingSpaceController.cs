using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationServices.Queries;

namespace Web.Controllers
{
    public class ViewParkingSpaceController : Controller
    {
        private IMediator _mediator;

        public ViewParkingSpaceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET
        public async Task<IActionResult> Index(int id)
        {
            var query = new GetSingleParkingSpaceQuery(id);
            var result = await _mediator.Send(query);
            return View(result);
        }
    }
}