using System;
using System.Threading.Tasks;
using ApplicationServices.Commands;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.Infrastructure.Data;

namespace ApplicationServices.Tests
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

        public static RegisterNewParkingSpaceCommand GetTestCreateParkingSpaceCommand(string userId)
        {
            var address = GetTestAddress();
            var rate = GetTestBookingRate();
            var description = GetTestDescription();
            var availability = GetTestAvailability();

            return new RegisterNewParkingSpaceCommand(userId, description, address, availability, rate);
        }

        public static DbContextOptions<ParkMateDbContext> GetUniqueDbContextOptions()
        {
            return GetNamedDbContextOptions(Guid.NewGuid().ToString());
        }

        public static DbContextOptions<ParkMateDbContext> GetNamedDbContextOptions(string name)
        {
            return new DbContextOptionsBuilder<ParkMateDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
        }

        public static async Task CreateTestParkingSpaceInDb(string name)
        {
            using (var context = new ParkMateDbContext(GetNamedDbContextOptions(name)))
            {
                var command = GetTestCreateParkingSpaceCommand("test-user");
                var repository = new ParkingSpaceRepository(context);
                var handler = new RegisterNewParkingSpaceCommandHandler(repository);
                await handler.Handle(command);
            }
        }
    }
}
