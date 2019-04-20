using System;
using System.Threading.Tasks;
using ParkMate.ApplicationServices.Commands;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.Infrastructure.Data;
using Moq;
using MediatR;
using ParkMate.ApplicationServices.DTOs;

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
            return new Vehicle("Testla", "Test3", "Blue", "TSTR");
        }

        public static AddressDTO GetTestAddressDTO()
        {
            return new AddressDTO
            {
                Street = "123 Test Street", 
                City = "TestVille", 
                State = "ABC", 
                Zip = "12345", 
                Latitude = 1.2d, 
                Longitude = 3.4
            };
        }
        public static AddressDTO GetTestAddressWithStreet(string street)
        {
            var address = GetTestAddressDTO();
            address.Street = street;
            return address;
        }
        public static BookingRateDTO GetTestBookingRateDTO()
        {
            return new BookingRateDTO
            {
                HourlyRate = 11,
                DailyRate = 22
            };
        }

        public static DescriptionDTO GetTestDescriptionDTO()
        {
            return new DescriptionDTO
            {
                Title = "Test Title",
                Description = "Test Description",
                ImageURL = "http://www.test.com/test.png"
            };
        }


        public static VehicleDTO GetTestVehicleDTO()
        {
            return new VehicleDTO 
            { 
                Make = "Testyota", 
                Model = "CamryUnit", 
                Color = "Green", 
                Registration = "TDD123" 
            };
        }

        public static ParkingSpaceDTO GetTestParkingSpaceDTO(string userId)
        {
            return new ParkingSpaceDTO
            {
                OwnerId = userId,
                Description = GetTestDescriptionDTO(),
                Address = GetTestAddressDTO(),
                BookingRate = GetTestBookingRateDTO()
            };
        }

        public static RegisterNewParkingSpaceCommand GetTestCreateParkingSpaceCommand(string userId)
        {
            return new RegisterNewParkingSpaceCommand(GetTestParkingSpaceDTO(userId));
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

                var vehicleCommand = new AddNewVehicleCommand(id, GetTestVehicleDTO());
                var vehicleHandler = new AddNewVehicleCommandHandler(repository, new Mock<IMediator>().Object);
                await vehicleHandler.Handle(vehicleCommand);
            }
        }
    }
}
