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
using Microsoft.AspNetCore.Authorization;
using ParkMate.Web.Enums;

namespace ParkMate.Web.Controllers
{
    [Authorize]
    public class EditParkingSpaceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ImageProcessor _imageProcessor;
        private readonly string _userId;

        public EditParkingSpaceController(
            IMediator mediator,
            ImageProcessor imageProcessor)
        {
            _mediator = mediator;
            _imageProcessor = imageProcessor;
            _userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            
            var command = new EditParkingSpaceAddressCommand(parkingSpaceId, _userId, address);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","MyParkingSpaces", new
            {
                PreviousCommandPresent = true,
                PreviousCommandResult = result.Success,
                PreviousCommandMessage = result.Message
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDescription([FromForm] DescriptionDTO dto, int parkingSpaceId)
        {
            var imageResult =  await _imageProcessor.SaveImage(dto.ImageFile);
            dto.ImageURL = imageResult.FileName;
            
            var description = new ParkingSpaceDescription(dto.Title, dto.Description, dto.ImageURL);
            
            var command = new EditParkingSpaceDescriptionCommand(parkingSpaceId, _userId, description);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","MyParkingSpaces", new
            {
                PreviousCommandPresent = true,
                PreviousCommandResult = result.Success,
                PreviousCommandMessage = result.Message
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBookingRate([FromForm] BookingRateDTO dto, int parkingSpaceId)
        {
            var rate = new BookingRate(new Money(dto.HourlyRate), new Money(dto.DailyRate));
            
            var command = new EditParkingSpaceBookingRateCommand(parkingSpaceId, _userId, rate);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","MyParkingSpaces", new
            {
                PreviousCommandPresent = true,
                PreviousCommandResult = result.Success,
                PreviousCommandMessage = result.Message
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetVisibility(bool isVisible, int parkingSpaceId)
        {
            var command = new SetParkingSpaceVisibilityCommand(parkingSpaceId, _userId, isVisible);

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                
            }

            return RedirectToAction("Index","MyParkingSpaces", new
            {
                PreviousCommandPresent = true,
                PreviousCommandStatus = result.Success,
                PreviousCommandMessage = result.Message
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAvailability(List<AvailableTimeDTO> days, int parkingSpaceId)
        {
            var updatedDays = days
                .Select(d => AvailabilityTime
                .CreateAvailabilityWithHours(d.Day, d.AvailableFrom, d.AvailableTo))
                .ToList();

            var command = new EditParkingSpaceAvailabilityCommand(parkingSpaceId, _userId, updatedDays);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","MyParkingSpaces", new
            {
                PreviousCommandPresent = true,
                PreviousCommandResult = result.Success,
                PreviousCommandMessage = result.Message
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteParkingSpace(int parkingSpaceId)
        {
            var command = new DeleteParkingSpaceCommand(parkingSpaceId, _userId);

            var result = await _mediator.Send(command);

            return RedirectToAction("Index","MyParkingSpaces", new
            {
                PreviousCommandPresent = true,
                PreviousCommandResult = result.Success,
                PreviousCommandMessage = result.Message
            });
        }

    }
}