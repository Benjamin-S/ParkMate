using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationServices.DTOs;
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}