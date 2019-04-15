using System;
using Xunit;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationCore.Exceptions;
using Moq;
using ParkMate.ApplicationCore.Entities;
using static ApplicationCore.Tests.TestHelper;
using System.Linq;
using ParkMate.ApplicationCore.Util;

namespace ApplicationCore.Tests
{
    public class ParkingSpaceShould : IDisposable
    {

        public ParkingSpaceShould()
        {
            SystemTime.SetDateTime(new DateTime(2019, 1, 15));
        }

        public void Dispose()
        {
            SystemTime.ResetDateTime();
        }

        [Fact]
        public void AddNewBooking()
        {
            var space = GetTestParkingSpace("test");
            var booking = new Booking(space, GetTestVehicle(),
                new BookingPeriod(DateTime.Now, DateTime.Now.AddHours(1), new Money()));

            space.AddBookingToSchedule(booking);

            Assert.Single(space.Bookings);
        }

        [Fact]
        public void ThrowExceptionWhenAddingOverlappingBooking()
        {
            var space = GetTestParkingSpace("test");
            var booking = new Booking(space, GetTestVehicle(),
                new BookingPeriod(SystemTime.Now(), SystemTime.Now().AddHours(2), new Money()));
            var booking2 = new Booking(space, GetTestVehicle(),
                new BookingPeriod(SystemTime.Now().AddHours(1), SystemTime.Now().AddHours(3), new Money()));

            space.AddBookingToSchedule(booking);

            Assert.Throws<AlreadyBookedException>(() => space.AddBookingToSchedule(booking2));
        }

        [Fact]
        public void ReturnTrueWhenSpaceIsAvailable()
        {
            var space = GetTestParkingSpace("test");
            space.AddBookingToSchedule(new Booking(space, GetTestVehicle(),
                new BookingPeriod(SystemTime.Now(), SystemTime.Now().AddHours(2), new Money())));

            var period = new BookingPeriod(SystemTime.Now().AddHours(3), SystemTime.Now().AddHours(4), new Money());

            Assert.True(space.IsAvailable(period));
        }

        [Fact]
        public void ReturnFalseWhenSpaceIsAlreadyBooked()
        {
            var space = GetTestParkingSpace("test");
            var booking = new Booking(space, GetTestVehicle(),
                new BookingPeriod(SystemTime.Now(), SystemTime.Now().AddHours(2), new Money()));
            var period = new BookingPeriod(SystemTime.Now().AddHours(1), SystemTime.Now().AddHours(3), new Money());

            space.AddBookingToSchedule(booking);

            Assert.False(space.IsAvailable(period));
        }

        [Fact]
        public void ReturnFalseWhenSpaceIsNotAvailable()
        {
            var space = GetTestParkingSpace("test");
            space.Availability.SetAvailabilityForDay(AvailabilityTime.CreateUnavailableDay(DayOfWeek.Tuesday));
            var period = new BookingPeriod(SystemTime.Now(), SystemTime.Now().AddHours(2), new Money());

            Assert.False(space.IsAvailable(period));
        }
    }
}
