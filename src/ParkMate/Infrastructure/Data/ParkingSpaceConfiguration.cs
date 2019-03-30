﻿using ParkMate.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ParkMate.Infrastructure.Data
{
    public class ParkingSpaceConfiguration : IEntityTypeConfiguration<ParkingSpace>
    {
        public void Configure(EntityTypeBuilder<ParkingSpace> builder)
        {
            builder.OwnsOne(ps => ps.Address, ad =>
            {
                ad.Property(a => a.Street).IsRequired();
                ad.Property(a => a.City).IsRequired();
                ad.Property(a => a.State).IsRequired();
                ad.Property(a => a.Zip).IsRequired();
            });
            builder.OwnsOne(ps => ps.Description, des => 
            {
                des.Property(d => d.Description).IsRequired();
                des.Property(d => d.Title).IsRequired();
                des.Property(d => d.ImageURL).IsRequired();
            });
            builder.OwnsOne(ps => ps.BookingRate, br =>
            {
                br.OwnsOne(m => m.DailyRate);
                br.OwnsOne(m => m.HourlyRate);
            });
            builder.Property(o => o.OwnerId).IsRequired();
        }
    }
}
