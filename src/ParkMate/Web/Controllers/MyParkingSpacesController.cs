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

        public IActionResult CreateParkingSpace()
        {
            return View();
        }

        public Task<IActionResult> ButtonAction()
        {
            var result = new Result();
            result.ResultType = ResultType.Command;
            result.Success = true;
            result.Message = "YAYAYA";

            return Index(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParkingSpace([FromForm] CreateParkingSpaceDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            //var imageResult =  await _imageProcessor.SaveImage(dto.Description.ImageFile);
            dto.Description.ImageURL = "test.jpg"; //imageResult.FileName;

            var result = await _mediator.Send(BuildParkingSpaceCommand(dto));

            return await Index(result);
        }

        RegisterNewParkingSpaceCommand BuildParkingSpaceCommand(CreateParkingSpaceDTO dto)
        {
            return new RegisterNewParkingSpaceCommand(
                User.FindFirst(ClaimTypes.NameIdentifier).Value,

                new ParkingSpaceDescription(dto.Description.Title,
                    dto.Description.Description, dto.Description.ImageFile.FileName),

                new Address(dto.Address.Street, dto.Address.City, dto.Address.State,
                    dto.Address.Zip, new Point(dto.Address.Latitude, dto.Address.Longitude)),

                SpaceAvailability.Create247Availability(),
                new BookingRate(
                    new Money(dto.BookingRate.HourlyRate),
                    new Money(dto.BookingRate.DailyRate)));

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAddress([FromForm] AddressDTO dto, int parkingSpaceId)
        {
            var address = new Address(dto.Street, dto.City, dto.State, dto.Zip, new Point(dto.Latitude, dto.Longitude));
            
            var command = new EditParkingSpaceAddressCommand(parkingSpaceId, _userId, address);

            var result = await _mediator.Send(command);

            return await Index(result);
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

            return await Index(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBookingRate([FromForm] BookingRateDTO dto, int parkingSpaceId)
        {
            var rate = new BookingRate(new Money(dto.HourlyRate), new Money(dto.DailyRate));
            
            var command = new EditParkingSpaceBookingRateCommand(parkingSpaceId, _userId, rate);

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
            var updatedDays = days
                .Select(d => AvailabilityTime
                .CreateAvailabilityWithHours(d.Day, d.AvailableFrom, d.AvailableTo))
                .ToList();

            var command = new EditParkingSpaceAvailabilityCommand(parkingSpaceId, _userId, updatedDays);

            var result = await _mediator.Send(command);

            return await Index(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteParkingSpace(int parkingSpaceId)
        {
            var command = new DeleteParkingSpaceCommand(parkingSpaceId, _userId);

            var result = await _mediator.Send(command);

            return await Index(result);
        }
    }
}
