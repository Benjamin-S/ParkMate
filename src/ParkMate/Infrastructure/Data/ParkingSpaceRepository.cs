using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.Infrastructure.Data
{
    public class ParkingSpaceRepository : WriteRepository<ParkingSpace>
    {
        public ParkingSpaceRepository(ParkMateDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public override async Task<ParkingSpace> GetByIdAsync(int id)
        {
            return await DbContext.ParkingSpaces
                .Include(p => p.Availability).
                SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}