using System.Diagnostics;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Queries;
using ParkMate.Web.Models;

namespace ParkMate.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DistanceSearchDTO dto)
        {
            var query = new FindSpacesWithinDistanceQuery(dto);
            var result = await _mediator.Send(query);

            return RedirectToAction("SearchResult", "Search", new
            {
                viewModel = new SearchResultViewModel()
                {
                    PrevInput = dto,
                    Result = result
                }
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}