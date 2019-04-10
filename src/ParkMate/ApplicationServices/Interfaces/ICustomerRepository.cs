using System.Threading.Tasks;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByIdAsync(string id);
    }
}
