using System;
using System.Threading;
using System.Threading.Tasks;
using ParkMate.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationServices;
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
        public DbSet<Booking> Bookings { get; private set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<BookingHistory> BookingHistory { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParkingSpaceConfiguration());
            modelBuilder.ApplyConfiguration(new SpaceAvailabilityConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.HasPostgresExtension("postgis");
        }

        public async Task<bool> SaveEntitiesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.SaveChangesAsync();

            return true;
        }
    }
}
