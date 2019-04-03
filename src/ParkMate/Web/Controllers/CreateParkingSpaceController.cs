using System.Threading.Tasks;
using ParkMate.ApplicationServices.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MediatR;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationCore.Entities;
using ParkMate.Web.Models;
using ParkMate.Web.Util;

namespace ParkMate.Web.Controllers
{
    public class CreateParkingSpaceController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IMediator _mediator;
        private readonly ImageProcessor _imageProcessor;

        public CreateParkingSpaceController(
            IHostingEnvironment environment,
            IMediator mediator,
            ImageProcessor imageProcessor)
        {
            _environment = environment;
            _mediator = mediator;
            _imageProcessor = imageProcessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CreateParkingSpaceDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var imageResult =  await _imageProcessor.SaveImage(dto.Description.ImageFile);
            dto.Description.ImageURL = imageResult.FileName;

            var result = await _mediator.Send(BuildParkingSpaceCommand(dto)); 

            return RedirectToAction(nameof(Index));
        }

        RegisterNewParkingSpaceCommand BuildParkingSpaceCommand(CreateParkingSpaceDTO dto)
        {
            return new RegisterNewParkingSpaceCommand(
                User.FindFirst(ClaimTypes.NameIdentifier).ToString(),

                new ParkingSpaceDescription(dto.Description.Title,
                    dto.Description.Description, dto.Description.ImageFile.FileName),

                new Address(dto.Address.Street, dto.Address.City, dto.Address.State,
                    dto.Address.Zip, new Point(dto.Address.Latitude, dto.Address.Longitude)),

                SpaceAvailability.Create247Availability(),
                    new BookingRate(
                        new Money(dto.BookingRate.HourlyRate),
                        new Money(dto.BookingRate.DailyRate)));

        }
    }
}
