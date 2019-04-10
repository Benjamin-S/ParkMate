using System;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;

namespace ApplicationCore.Tests
{
    public static class TestHelper
    {
        public static Address GetTestAddress()
        {
            return new Address(
                "123 Test Street", "TestVille", "ABC", "12345", new Point(1.2, 3.4));
        }

        public static BookingRate GetTestBookingRate()
        {
            return new BookingRate(new Money(11), new Money(22));
        }

        public static ParkingSpaceDescription GetTestDescription()
        {
            return new ParkingSpaceDescription(
                "Test Title", "Test Description", "http://www.test.com/test.png");
        }

        public static SpaceAvailability GetTestAvailability()
        {
            return SpaceAvailability.Create247Availability();
        }

        public static Vehicle GetTestVehicle()
        {
            return new Vehicle("Testla", "Test3", "Blue", "TSTR");
        }

        public static ParkingSpace GetTestParkingSpace(string userId)
        {
            var address = GetTestAddress();
            var rate = GetTestBookingRate();
            var description = GetTestDescription();
            var availability = GetTestAvailability();

            return new ParkingSpace(userId, description, address, availability, rate);
        }
    }
}
