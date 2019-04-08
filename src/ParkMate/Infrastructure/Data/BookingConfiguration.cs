using ParkMate.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ParkMate.Infrastructure.Data
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> booking)
        {
            booking.OwnsOne(b => b.BookingPeriod, bp =>
                bp.OwnsOne(m => m.Charge));
        }
    }
}
