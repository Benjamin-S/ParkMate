using System;
using System.Threading.Tasks;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface IParkingSpaceRepository : IRepository<ParkingSpace>
    {
        Task<ParkingSpace> GetByIdAsync(int id);
    }
}
