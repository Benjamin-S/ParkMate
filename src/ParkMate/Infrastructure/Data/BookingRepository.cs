using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public class BookingRepository : WriteRepository<Booking>, IBookingRepository 
    {
        public BookingRepository(ParkMateDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await DbContext.Bookings
               .Include(b => b.BookingInfo)
               .Include(b => b.ParkingSpace)
               .Include(b => b.Vehicle)
               .SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
