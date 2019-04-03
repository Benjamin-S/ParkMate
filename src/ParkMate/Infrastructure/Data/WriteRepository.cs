using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public abstract class WriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected ParkMateDbContext DbContext { get; set; }

        public IUnitOfWork UnitOfWork => DbContext;

        public abstract Task<T> GetByIdAsync(int id);

        public async Task<T> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            DbContext.Update(entity);
        }

        public void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }
    }
}
