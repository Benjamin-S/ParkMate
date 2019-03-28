using ParkMate.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ParkMate.Infrastructure.Data
{
    public class SpaceAvailabilityConfiguration : IEntityTypeConfiguration<SpaceAvailability>
    {
        public void Configure(EntityTypeBuilder<SpaceAvailability> spaceAvailability)
        {
            spaceAvailability.OwnsOne(a => a.Monday);
            spaceAvailability.OwnsOne(a => a.Monday);
            spaceAvailability.OwnsOne(a => a.Tuesday);
            spaceAvailability.OwnsOne(a => a.Wednesday);
            spaceAvailability.OwnsOne(a => a.Thursday);
            spaceAvailability.OwnsOne(a => a.Friday);
            spaceAvailability.OwnsOne(a => a.Saturday);
            spaceAvailability.OwnsOne(a => a.Sunday);
        }
    }
}
