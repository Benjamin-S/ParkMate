using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices;
using ParkMate.ApplicationServices.Queries;
using ParkMate.Web.Enums;

namespace Web.Models
{
    public class ResultViewModel<T>
    {
        public Result Command { get; set; }
        public Result<T> Query { get; set; }
    }
}