using System.Collections.Generic;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.DTOs;

namespace Web.Models
{
    public class SearchResultViewModel
    {
        public DistanceSearchDTO PrevInput;
        public Result<IReadOnlyList<ParkingSpaceViewModel>> Result { get; set; }
    }
}