using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public class ParkingSpaceRepository : WriteRepository<ParkingSpace>, IParkingSpaceRepository
    {
        public ParkingSpaceRepository(ParkMateDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<ParkingSpace> GetByIdAsync(int id, string ownerId)
        {
            return await DbContext.ParkingSpaces
                .Include(p => p.Availability).
                SingleOrDefaultAsync(e => e.Id == id && e.OwnerId.Equals(ownerId));
        }
    }
}