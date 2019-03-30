using System;
using System.Threading;
using System.Threading.Tasks;
using ParkMate.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationServices.Interfaces;

namespace ParkMate.Infrastructure.Data
{
    public class ParkMateDbContext : DbContext, IUnitOfWork
    {
        public ParkMateDbContext(DbContextOptions<ParkMateDbContext> options) 
            : base(options)
        {
        }
        public DbSet<ParkingSpace> ParkingSpaces { get; private set; }
        public DbSet<SpaceAvailability> SpaceAvailability { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParkingSpaceConfiguration());
            modelBuilder.ApplyConfiguration(new SpaceAvailabilityConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.SaveChangesAsync();

            return true;
        }
    }
}
