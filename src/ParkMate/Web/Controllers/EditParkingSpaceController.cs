using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.Queries;
using ParkMate.Web.Models;
using ParkMate.Web.Util;

namespace ParkMate.Web.Controllers
{
    public class EditParkingSpaceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ImageProcessor _imageProcessor;

        public EditParkingSpaceController(
            IMediator mediator,
            ImageProcessor imageProcessor)
        {
            _mediator = mediator;
            _imageProcessor = imageProcessor;
        }

        public IActionResult EditAddress()
        {
            return View();
        }
        
        public IActionResult EditAvailability()
        {
            return View();
        }
        
        public IActionResult EditBookingRate()
        {
            return View();
        }
        
        public IActionResult EditDescription()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAddress([FromForm] AddressDTO dto, int parkingSpaceId)
        {
            var address = new Address(dto.Street, dto.City, dto.State, dto.Zip, new Point(dto.Latitude, dto.Longitude));
            
            var command = new EditParkingSpaceAddressCommand(parkingSpaceId, address);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","ViewParkingSpace", new {id = parkingSpaceId});
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDescription([FromForm] DescriptionDTO dto, int parkingSpaceId)
        {
            var imageResult =  await _imageProcessor.SaveImage(dto.ImageFile);
            dto.ImageURL = imageResult.FileName;
            
            var description = new ParkingSpaceDescription(dto.Title, dto.Description, dto.ImageURL);
            
            var command = new EditParkingSpaceDescriptionCommand(parkingSpaceId, description);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","ViewParkingSpace", new {id = parkingSpaceId});
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBookingRate([FromForm] BookingRateDTO dto, int parkingSpaceId)
        {
            var rate = new BookingRate(new Money(dto.HourlyRate), new Money(dto.DailyRate));
            
            var command = new EditParkingSpaceBookingRateCommand(parkingSpaceId, rate);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","ViewParkingSpace", new {id = parkingSpaceId});
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetVisibility(bool isVisible, int parkingSpaceId)
        {
            var command = new SetParkingSpaceVisibilityCommand(parkingSpaceId, isVisible);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","ViewParkingSpace", new {id = parkingSpaceId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAvailability(List<AvailableTimeDTO> days, int parkingSpaceId)
        {
            var updatedDays = days
                .Select(d => AvailabilityTime
                .CreateAvailabilityWithHours(d.Day, d.AvailableFrom, d.AvailableTo))
                .ToList();

            var command = new EditParkingSpaceAvailabilityCommand(parkingSpaceId, updatedDays);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","ViewParkingSpace", new {id = parkingSpaceId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteParkingSpace(int parkingSpaceId)
        {
            var command = new DeleteParkingSpaceCommand(parkingSpaceId);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","MyParkingSpaces");
        }

    }
}