using System;
using ParkMate.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ParkMate.Infrastructure.Data
{
    public class ParkMateDbContext : DbContext
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
    }
}
