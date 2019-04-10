using System.Collections.Generic;
using System.Threading.Tasks;
using ParkMate.ApplicationCore.Entities;

namespace ParkMate.ApplicationServices.Interfaces
{
    public interface IRepository<T> where T : BaseEntity 
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
    
}