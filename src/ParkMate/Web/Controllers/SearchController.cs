using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ParkMate.Web.Controllers

{
    public class SearchController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }      

        public IActionResult SearchAutoComplete(string searchInput)
        {
            searchInput = searchInput?.ToLower().Trim();
            var matchedObjects = new List<object>();
            
            // Match input to search terms, then return a list containing all matched objects and
            // return it as JSON

            return Json(matchedObjects);
        }
    }
}