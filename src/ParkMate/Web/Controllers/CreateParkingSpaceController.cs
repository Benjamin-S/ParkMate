using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationServices.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationCore.Entities;
using ParkMate.Web.Models;

namespace ParkMate.Web.Controllers
{
    public class CreateParkingSpaceController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly IMediator _mediator;

        public CreateParkingSpaceController(
            IHostingEnvironment environment,
            IMediator mediator)
        {
            _environment = environment;
            _mediator = mediator;
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
            var command = await BuildParkingSpaceCommand(dto);
            bool result = await _mediator.Send(command); 

            return RedirectToAction(nameof(Index));
        }

        async Task<RegisterNewParkingSpaceCommand> BuildParkingSpaceCommand(CreateParkingSpaceDTO dto)
        {
            var fileName = dto.Description.ImageFile.FileName;
            var filePath = Path.Combine(_environment.WebRootPath, "ImageUploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Description.ImageFile.CopyToAsync(stream);
            }
            var address = new Address(dto.Address.Street, dto.Address.City, dto.Address.State,
                dto.Address.Zip, dto.Address.Latitude, dto.Address.Longitude);

            var description = new ParkingSpaceDescription(dto.Description.Title,
                dto.Description.Description, dto.Description.ImageFile.FileName);

            var rate = new BookingRate(new Money(dto.BookingRate.HourlyRate),
                new Money(dto.BookingRate.DailyRate));

            var availability = SpaceAvailability.Create247Availability();

            return new RegisterNewParkingSpaceCommand("test-id", description, address, availability, rate);
        }
    }
}
