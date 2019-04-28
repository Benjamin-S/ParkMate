﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ParkMate.Infrastructure.Data;

namespace Infrastructure.Migrations.ParkMateDb
{
    [DbContext(typeof(ParkMateDbContext))]
    partial class ParkMateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:postgis", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookingHistoryId");

                    b.Property<DateTime>("BookingTime");

                    b.Property<string>("CustomerId");

                    b.Property<int?>("CustomerId1");

                    b.Property<int?>("ParkingSpaceId");

                    b.Property<int>("Status");

                    b.Property<int?>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("BookingHistoryId");

                    b.HasIndex("CustomerId1");

                    b.HasIndex("ParkingSpaceId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.BookingHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("BookingHistory");
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("IdentityId");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.ParkingSpace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AvailabilityId");

                    b.Property<int?>("CustomerId");

                    b.Property<string>("OwnerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AvailabilityId");

                    b.HasIndex("CustomerId");

                    b.ToTable("ParkingSpaces");
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.SpaceAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsVisible");

                    b.HasKey("Id");

                    b.ToTable("SpaceAvailability");
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Colour");

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<string>("Registration");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("ParkMate.ApplicationServices.DTOs.SearchAddressDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("SearchAddresses");
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.Booking", b =>
                {
                    b.HasOne("ParkMate.ApplicationCore.Entities.BookingHistory")
                        .WithMany("Bookings")
                        .HasForeignKey("BookingHistoryId");

                    b.HasOne("ParkMate.ApplicationCore.Entities.Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId1");

                    b.HasOne("ParkMate.ApplicationCore.Entities.ParkingSpace", "ParkingSpace")
                        .WithMany("Bookings")
                        .HasForeignKey("ParkingSpaceId");

                    b.HasOne("ParkMate.ApplicationCore.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.BookingInfo", "BookingInfo", b1 =>
                        {
                            b1.Property<int>("BookingId");

                            b1.Property<int>("BillingUnit");

                            b1.Property<int>("BookingUnits");

                            b1.Property<DateTime>("End");

                            b1.Property<DateTime>("Start");

                            b1.HasKey("BookingId");

                            b1.ToTable("Bookings");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.Booking")
                                .WithOne("BookingInfo")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.BookingInfo", "BookingId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("ParkMate.ApplicationCore.ValueObjects.Money", "Rate", b2 =>
                                {
                                    b2.Property<int>("BookingInfoBookingId");

                                    b2.Property<decimal>("Value");

                                    b2.HasKey("BookingInfoBookingId");

                                    b2.ToTable("Bookings");

                                    b2.HasOne("ParkMate.ApplicationCore.ValueObjects.BookingInfo")
                                        .WithOne("Rate")
                                        .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.Money", "BookingInfoBookingId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("ParkMate.ApplicationCore.ValueObjects.Money", "Total", b2 =>
                                {
                                    b2.Property<int>("BookingInfoBookingId");

                                    b2.Property<decimal>("Value");

                                    b2.HasKey("BookingInfoBookingId");

                                    b2.ToTable("Bookings");

                                    b2.HasOne("ParkMate.ApplicationCore.ValueObjects.BookingInfo")
                                        .WithOne("Total")
                                        .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.Money", "BookingInfoBookingId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.ParkingSpace", b =>
                {
                    b.HasOne("ParkMate.ApplicationCore.Entities.SpaceAvailability", "Availability")
                        .WithMany()
                        .HasForeignKey("AvailabilityId");

                    b.HasOne("ParkMate.ApplicationCore.Entities.Customer")
                        .WithMany("ParkingSpaces")
                        .HasForeignKey("CustomerId");

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<int>("ParkingSpaceId");

                            b1.Property<string>("City")
                                .IsRequired();

                            b1.Property<Point>("Location");

                            b1.Property<string>("State")
                                .IsRequired();

                            b1.Property<string>("Street")
                                .IsRequired();

                            b1.Property<string>("Zip")
                                .IsRequired();

                            b1.HasKey("ParkingSpaceId");

                            b1.ToTable("ParkingSpaces");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.ParkingSpace")
                                .WithOne("Address")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.Address", "ParkingSpaceId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.BookingRate", "BookingRate", b1 =>
                        {
                            b1.Property<int>("ParkingSpaceId");

                            b1.HasKey("ParkingSpaceId");

                            b1.ToTable("ParkingSpaces");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.ParkingSpace")
                                .WithOne("BookingRate")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.BookingRate", "ParkingSpaceId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("ParkMate.ApplicationCore.ValueObjects.Money", "DailyRate", b2 =>
                                {
                                    b2.Property<int>("BookingRateParkingSpaceId");

                                    b2.Property<decimal>("Value");

                                    b2.HasKey("BookingRateParkingSpaceId");

                                    b2.ToTable("ParkingSpaces");

                                    b2.HasOne("ParkMate.ApplicationCore.ValueObjects.BookingRate")
                                        .WithOne("DailyRate")
                                        .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.Money", "BookingRateParkingSpaceId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });

                            b1.OwnsOne("ParkMate.ApplicationCore.ValueObjects.Money", "HourlyRate", b2 =>
                                {
                                    b2.Property<int>("BookingRateParkingSpaceId");

                                    b2.Property<decimal>("Value");

                                    b2.HasKey("BookingRateParkingSpaceId");

                                    b2.ToTable("ParkingSpaces");

                                    b2.HasOne("ParkMate.ApplicationCore.ValueObjects.BookingRate")
                                        .WithOne("HourlyRate")
                                        .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.Money", "BookingRateParkingSpaceId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.ParkingSpaceDescription", "Description", b1 =>
                        {
                            b1.Property<int>("ParkingSpaceId");

                            b1.Property<string>("Description")
                                .IsRequired();

                            b1.Property<string>("ImageURL")
                                .IsRequired();

                            b1.Property<string>("Title")
                                .IsRequired();

                            b1.HasKey("ParkingSpaceId");

                            b1.ToTable("ParkingSpaces");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.ParkingSpace")
                                .WithOne("Description")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.ParkingSpaceDescription", "ParkingSpaceId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.SpaceAvailability", b =>
                {
                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "Friday", b1 =>
                        {
                            b1.Property<int>("SpaceAvailabilityId");

                            b1.Property<TimeSpan>("AvailableFrom");

                            b1.Property<TimeSpan>("AvailableTo");

                            b1.Property<int>("DayOfWeek");

                            b1.Property<bool>("IsAvailable");

                            b1.HasKey("SpaceAvailabilityId");

                            b1.ToTable("SpaceAvailability");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.SpaceAvailability")
                                .WithOne("Friday")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "SpaceAvailabilityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "Monday", b1 =>
                        {
                            b1.Property<int>("SpaceAvailabilityId");

                            b1.Property<TimeSpan>("AvailableFrom");

                            b1.Property<TimeSpan>("AvailableTo");

                            b1.Property<int>("DayOfWeek");

                            b1.Property<bool>("IsAvailable");

                            b1.HasKey("SpaceAvailabilityId");

                            b1.ToTable("SpaceAvailability");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.SpaceAvailability")
                                .WithOne("Monday")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "SpaceAvailabilityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "Saturday", b1 =>
                        {
                            b1.Property<int>("SpaceAvailabilityId");

                            b1.Property<TimeSpan>("AvailableFrom");

                            b1.Property<TimeSpan>("AvailableTo");

                            b1.Property<int>("DayOfWeek");

                            b1.Property<bool>("IsAvailable");

                            b1.HasKey("SpaceAvailabilityId");

                            b1.ToTable("SpaceAvailability");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.SpaceAvailability")
                                .WithOne("Saturday")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "SpaceAvailabilityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "Sunday", b1 =>
                        {
                            b1.Property<int>("SpaceAvailabilityId");

                            b1.Property<TimeSpan>("AvailableFrom");

                            b1.Property<TimeSpan>("AvailableTo");

                            b1.Property<int>("DayOfWeek");

                            b1.Property<bool>("IsAvailable");

                            b1.HasKey("SpaceAvailabilityId");

                            b1.ToTable("SpaceAvailability");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.SpaceAvailability")
                                .WithOne("Sunday")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "SpaceAvailabilityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "Thursday", b1 =>
                        {
                            b1.Property<int>("SpaceAvailabilityId");

                            b1.Property<TimeSpan>("AvailableFrom");

                            b1.Property<TimeSpan>("AvailableTo");

                            b1.Property<int>("DayOfWeek");

                            b1.Property<bool>("IsAvailable");

                            b1.HasKey("SpaceAvailabilityId");

                            b1.ToTable("SpaceAvailability");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.SpaceAvailability")
                                .WithOne("Thursday")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "SpaceAvailabilityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "Tuesday", b1 =>
                        {
                            b1.Property<int>("SpaceAvailabilityId");

                            b1.Property<TimeSpan>("AvailableFrom");

                            b1.Property<TimeSpan>("AvailableTo");

                            b1.Property<int>("DayOfWeek");

                            b1.Property<bool>("IsAvailable");

                            b1.HasKey("SpaceAvailabilityId");

                            b1.ToTable("SpaceAvailability");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.SpaceAvailability")
                                .WithOne("Tuesday")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "SpaceAvailabilityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "Wednesday", b1 =>
                        {
                            b1.Property<int>("SpaceAvailabilityId");

                            b1.Property<TimeSpan>("AvailableFrom");

                            b1.Property<TimeSpan>("AvailableTo");

                            b1.Property<int>("DayOfWeek");

                            b1.Property<bool>("IsAvailable");

                            b1.HasKey("SpaceAvailabilityId");

                            b1.ToTable("SpaceAvailability");

                            b1.HasOne("ParkMate.ApplicationCore.Entities.SpaceAvailability")
                                .WithOne("Wednesday")
                                .HasForeignKey("ParkMate.ApplicationCore.ValueObjects.AvailabilityTime", "SpaceAvailabilityId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("ParkMate.ApplicationCore.Entities.Vehicle", b =>
                {
                    b.HasOne("ParkMate.ApplicationCore.Entities.Customer")
                        .WithMany("Vehicles")
                        .HasForeignKey("CustomerId");
                });
#pragma warning restore 612, 618
        }
    }
}
