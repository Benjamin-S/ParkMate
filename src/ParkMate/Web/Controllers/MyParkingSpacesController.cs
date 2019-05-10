using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Queries;
using ParkMate.Web.Models;
using ParkMate.Web.Util;

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
            var viewModel = new ResultViewModel<CustomerViewModel>
            {
                Command = previousCommand,
                Query = await _mediator.Send(query)
            };
  
            return View("Index", viewModel);
        }
        
        public IActionResult CreateParkingSpace()
        {
            return View();
        }

        public async Task<IActionResult> EditAddress(int parkingSpaceId)
        {
            var query = new GetSingleParkingSpaceQuery(parkingSpaceId);
            var result = await _mediator.Send(query);
            return View(result.Payload);
        }
        
        public async Task<IActionResult> EditAvailability(int parkingSpaceId)
        {
            var query = new GetSingleParkingSpaceQuery(parkingSpaceId);
            var result = await _mediator.Send(query);
            return View(result.Payload);
        }
        
        public async Task<IActionResult> EditBookingRate(int parkingSpaceId)
        {
            var query = new GetSingleParkingSpaceQuery(parkingSpaceId);
            var result = await _mediator.Send(query);
            return View(result.Payload);
        }
        
        public async Task<IActionResult> EditDescription(int parkingSpaceId)
        {
            var query = new GetSingleParkingSpaceQuery(parkingSpaceId);
            var result = await _mediator.Send(query);
            return View(new ParkingSpaceDescriptionViewModel()
            {
                Description = new DescriptionDTO()
                {
                    Description = result.Payload.Description,
                    ImageURL = result.Payload.ImageURL,
                    Title = result.Payload.Title
                },
                ParkingSpaceId = result.Payload.ParkingSpaceId
            });
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParkingSpace([FromForm] CreateParkingSpaceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ImageFile != null)
            {
                var imageResult = _imageProcessor.SaveImage(model.ImageFile);
                model.ParkingSpace.Description.ImageURL =
                    imageResult.IsValid ? imageResult.FileName : "default.jpg";
            }
            else
            {
                model.ParkingSpace.Description.ImageURL = "default.jpg";
            }

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
            if (model.ImageFile != null)
            {
                var imageResult = _imageProcessor.SaveImage(model.ImageFile);
                model.Description.ImageURL = imageResult.IsValid ? imageResult.FileName : "default.jpg";
                model.Description.ImageURL = imageResult.FileName;
            }
            else
            {
                model.Description.ImageURL = "default.jpg";
            }
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
        public async Task<IActionResult> EditAvailability([FromForm] AvailabilityDTO days, int parkingSpaceId)
        {
            var command = new EditParkingSpaceAvailabilityCommand(parkingSpaceId, _userId, new List<AvailableTimeDTO>()
            {
                days.Monday,
                days.Tuesday,
                days.Wednesday,
                days.Thursday,
                days.Friday,
                days.Saturday,
                days.Sunday
            });
            var result = await _mediator.Send(command);
            return await Index(result);
        }
        
        public async Task<IActionResult> SetVisibility(bool isVisible, int parkingSpaceId)
        {
            var command = new SetParkingSpaceVisibilityCommand(parkingSpaceId, _userId, !isVisible);
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
