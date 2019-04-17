using System;
using System.Threading.Tasks;
using ParkMate.ApplicationServices.Commands;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.Infrastructure.Data;
using Moq;
using MediatR;

namespace ParkMate.ApplicationServices.Tests
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
            return new Vehicle("Testyota", "CamryUnit", "Green", "TDD123");
        }

        public static RegisterNewParkingSpaceCommand GetTestCreateParkingSpaceCommand(string userId)
        {
            var address = GetTestAddress();
            var rate = GetTestBookingRate();
            var description = GetTestDescription();
            var availability = GetTestAvailability();

            return new RegisterNewParkingSpaceCommand(userId, description, address, availability, rate);
        }

        public static Customer GetTestCustomer(string id)
        {
            return new Customer(id, "test@test.com", "Mr. Test");
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

        public static async Task CreateTestParkingSpaceInMemoryDb(string name, string ownerName = "test-user")
        {
            await CreateTestCustomerInMemoryDb(name, ownerName);
            using (var context = new ParkMateDbContext(GetNamedDbContextOptions(name)))
            {
                var command = GetTestCreateParkingSpaceCommand(ownerName);
                var repository = new CustomerRepository(context);
                var handler = new RegisterNewParkingSpaceCommandHandler(repository, new Mock<IMediator>().Object);
                await handler.Handle(command);
            }
        }
        public static async Task CreateTestCustomerInMemoryDb(string dbName, string id)
        {
            using (var context = new ParkMateDbContext(GetNamedDbContextOptions(dbName)))
            {
                var command = new RegisterCustomerCommand(id, "test@test.com", "Mr. Test");
                var repository = new CustomerRepository(context);
                var handler = new RegisterCustomerCommandHandler(repository, new Mock<IMediator>().Object);
                await handler.Handle(command);

                var vehicleCommand = new AddNewVehicleCommand(id, GetTestVehicle());
                var vehicleHandler = new AddNewVehicleCommandHandler(repository, new Mock<IMediator>().Object);
                await vehicleHandler.Handle(vehicleCommand);
            }
        }
    }
}
