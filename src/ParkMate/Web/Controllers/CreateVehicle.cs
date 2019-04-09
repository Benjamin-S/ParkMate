using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CreateVehicle : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return
            View();
        }
    }
}