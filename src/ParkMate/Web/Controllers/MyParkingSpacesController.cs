using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationServices.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Queries;
using ParkMate.Web.Enums;
using ParkMate.Web.Models;
using ParkMate.Web.Util;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class MyParkingSpacesController : Controller
    {
        private IMediator _mediator;
        private readonly ImageProcessor _imageProcessor;
        private string _userId; 
        
        public MyParkingSpacesController(
            IMediator mediator,
            ImageProcessor imageProcessor,
            IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _imageProcessor = imageProcessor;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<IActionResult> Index(Result previousCommand) {
            var query = new GetCustomerQuery(_userId);
            var viewModel = new ResultViewModel<Customer>
            {
                Command = previousCommand,
                Query = await _mediator.Send(query)
            };
  
            return View("Index", viewModel);
        }
        
        public IActionResult EditAddress()
        {
            return View();
        }
        
        public IActionResult EditAvailableDays()
        {
            return View();
        }
        
        public IActionResult EditAvailableTimes()
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

        public IActionResult CreateParkingSpace()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParkingSpace([FromForm] CreateParkingSpaceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var imageResult = _imageProcessor.SaveImage(model.ImageFile);
            model.ParkingSpace.Description.ImageURL = imageResult.FileName;

            model.ParkingSpace.OwnerId = _userId;

            var command = new RegisterNewParkingSpaceCommand(model.ParkingSpace);

            var result = await _mediator.Send(command);

            return await Index(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAddress([FromForm] AddressDTO dto, int parkingSpaceId)
        {
            var command = new EditParkingSpaceAddressCommand(parkingSpaceId, _userId, dto);

            var result = await _mediator.Send(command);

            return await Index(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDescription([FromForm] ParkingSpaceDescriptionViewModel model, int parkingSpaceId)
        {
            var imageResult = _imageProcessor.SaveImage(model.ImageFile);
            model.Description.ImageURL = imageResult.FileName;
            
            var command = new EditParkingSpaceDescriptionCommand(parkingSpaceId, _userId, model.Description);

            var result = await _mediator.Send(command);

            return await Index(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBookingRate([FromForm] BookingRateDTO dto, int parkingSpaceId)
        {
            var command = new EditParkingSpaceBookingRateCommand(parkingSpaceId, _userId, dto);

            var result = await _mediator.Send(command);

            return await Index(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetVisibility(bool isVisible, int parkingSpaceId)
        {
            var command = new SetParkingSpaceVisibilityCommand(parkingSpaceId, _userId, isVisible);

            var result = await _mediator.Send(command);

            return await Index(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAvailability(List<AvailableTimeDTO> days, int parkingSpaceId)
        {
            var command = new EditParkingSpaceAvailabilityCommand(parkingSpaceId, _userId, days);

            var result = await _mediator.Send(command);

            return await Index(result);
        }

        public async Task<IActionResult> DeleteParkingSpace(int parkingSpaceId)
        {
            var command = new DeleteParkingSpaceCommand(parkingSpaceId, _userId);

            var result = await _mediator.Send(command);

            return await Index(result);
        }
    }
}
