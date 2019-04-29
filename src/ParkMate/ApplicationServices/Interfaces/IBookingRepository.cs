using System.Threading.Tasks;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> GetByIdAsync(int id);
    }
}
