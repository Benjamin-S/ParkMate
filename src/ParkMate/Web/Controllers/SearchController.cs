using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.ApplicationServices.Queries;
using ParkMate.Web.Models;

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

        public IActionResult SearchResult(SearchResultViewModel viewModel)
        {
            return View("SearchResult", viewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DistanceSearchDTO dto)
        {
            var query = new FindSpacesWithinDistanceQuery(dto);
            var result = await _mediator.Send(query);

            return SearchResult(new SearchResultViewModel()
            {
                PrevInput = dto,
                Result = result
            });
        }

        public async Task<IActionResult> SearchAutoComplete(string searchInput)
        {
            var query = new GetAddressForStreetQuery(searchInput);
            var result = await _mediator.Send(query);

            return Json(result.Payload);
        }
    }
}