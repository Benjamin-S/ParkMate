using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public class CustomerRepository : WriteRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ParkMateDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await DbContext.Customers
                .Include(c => c.Vehicles)
                .Include(c => c.Bookings)
                .Include(c => c.BookingHistory)
                .SingleOrDefaultAsync(c => c.IdentityId.Equals(id));
        }
    }
}