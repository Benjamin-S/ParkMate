using ParkMate.ApplicationServices;

namespace ParkMate.Web.Models
{
    public class ResultViewModel<T>
    {
        public Result Command { get; set; }
        public Result<T> Query { get; set; }
    }
}