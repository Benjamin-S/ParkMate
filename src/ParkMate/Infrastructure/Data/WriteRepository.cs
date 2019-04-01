using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public class WriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ParkMateDbContext _dbContext;
        
        public WriteRepository(ParkMateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;
       

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}