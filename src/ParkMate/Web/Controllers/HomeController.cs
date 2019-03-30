using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkMate.Web.Models;

namespace ParkMate.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult SearchAutoComplete(string searchInput)
        {
            // FOR DEMONSTRARATION OF FUNCTIONALITY ONLY
            AutoCompleteDemonstration[] locationStrings = new AutoCompleteDemonstration[6];
            for(int i = 0; i < locationStrings.Length; i++)
            {
                locationStrings[i] = new AutoCompleteDemonstration();
            }
            
            locationStrings[0].Id = 1;
            locationStrings[0].Address = "Melbourne";
            locationStrings[1].Id = 2;
            locationStrings[1].Address = "Melton";
            locationStrings[2].Id = 3;
            locationStrings[2].Address = "Mellow";
            locationStrings[3].Id = 4;
            locationStrings[3].Address = "Melting";
            locationStrings[4].Id = 5;
            locationStrings[4].Address = "Brunswick";
            locationStrings[5].Id = 6;
            locationStrings[5].Address = "Carlton";
            // DELETE BETWEEN THESE LINES
            
            searchInput = searchInput?.ToLower().Trim();
            var matchedObjects = new List<AutoCompleteDemonstration>();

            for (int i = 0; i < locationStrings.Length; i++)
            {
                if (locationStrings[i].Address.ToLower().StartsWith(searchInput))
                {
                    matchedObjects.Add(locationStrings[i]);
                }
            }

            return Json(matchedObjects);
        }

        // FOR DEMONSTRATION ONLY
        public class AutoCompleteDemonstration
        {
            public int Id { get; set; }
            public string Address { get; set; }

            public AutoCompleteDemonstration()
            {
            }

            public AutoCompleteDemonstration(int id, string address)
            {
                Id = id;
                Address = address;
            }
        }
        // DELETE BETWEEN THESE LINES

    }
}