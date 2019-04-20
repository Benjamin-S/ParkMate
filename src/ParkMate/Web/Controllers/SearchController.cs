using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Queries;

namespace ParkMate.Web.Controllers

{
    public class SearchController : Controller
    {
        private IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchResult(Result<IReadOnlyList<ParkingSpaceListingDTO>> dto)
        {
            return View("SearchResult", dto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DistanceSearchDTO dto)
        {
            var query = new FindSpacesWithinDistanceQuery(dto);
            var result = await _mediator.Send(query);

            return SearchResult(result);
        }

        public async Task<IActionResult> SearchAutoComplete(string searchInput)
        {
            var query = new GetAddressForStreetQuery(searchInput);
            var result = await _mediator.Send(query);

            return Json(result.Payload);
        }
    }
}