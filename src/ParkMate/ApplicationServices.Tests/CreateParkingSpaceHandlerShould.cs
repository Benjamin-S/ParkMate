using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using Xunit;
using ApplicationServices.Commands;
using System.Threading;

namespace ApplicationServices.Tests
{
    public class CreateParkingSpaceHandlerShould
    {
        [Fact]
        public async Task CreateNewParkingSpace()
        {
            var command = GetTestCreateParkingSpaceCommand("test-user");

            using (var context = new ParkMateDbContext(GetDbContextOptions("CreateNewParkingSpace")))
            {
                var repository = new WriteRepository<BaseEntity>(context);
                var handler = new CreateParkingSpaceHandler(repository);
                await handler.Handle(command, default(CancellationToken));
            }

            using (var context = new ParkMateDbContext(GetDbContextOptions("CreateNewParkingSpace")))
            {
                Assert.Equal(1, context.ParkingSpaces.Count());
                var space = context.ParkingSpaces.Include(s => s.Availability).FirstOrDefault();

                Assert.NotNull(space.Availability);
                Assert.Equal(GetTestAddress(), space.Address);
                Assert.Equal(GetTestBookingRate(), space.BookingRate);
                Assert.Equal(GetTestDescription(), space.Description);
                Assert.Equal(GetTestAddress(), space.Address);
                Assert.Equal("test-user", space.OwnerId);
            }
        }        
        
        Address GetTestAddress()
        {
            return new Address(
                "123 Test Street", "TestVille", "ABC", "12345", 1.2, 3.4);
        }

        BookingRate GetTestBookingRate()
        {
            return new BookingRate(new Money(), new Money());
        }

        ParkingSpaceDescription GetTestDescription()
        {
            return new ParkingSpaceDescription(
                "Test Title", "Test Description", "http://www.test.com/test.png");
        }

        SpaceAvailability GetTestAvailability()
        {
            return SpaceAvailability.Create247Availability();
        }

        CreateParkingSpace GetTestCreateParkingSpaceCommand(string userId)
        {
            var address = GetTestAddress();
            var rate = GetTestBookingRate();
            var description = GetTestDescription();
            var availability = GetTestAvailability();

            return new CreateParkingSpace(userId, description, address, availability, rate);
        }

        DbContextOptions<ParkMateDbContext> GetDbContextOptions(string name)
        {
            return new DbContextOptionsBuilder<ParkMateDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;
        }
    }
}