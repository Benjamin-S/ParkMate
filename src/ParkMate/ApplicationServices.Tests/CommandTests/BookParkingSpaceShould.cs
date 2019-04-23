using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Commands;
using ParkMate.ApplicationServices.DTOs;
using ParkMate.Infrastructure.Data;
using Xunit;
using static ParkMate.ApplicationServices.Tests.TestHelper;

namespace ParkMate.ApplicationServices.Tests
{
    public class BookParkingSpaceShould : IAsyncLifetime
    {
        public async Task InitializeAsync()
        {
            await CreateTestParkingSpaceInMemoryDb("BookingParkingSpaceShould");
        }

        public async Task DisposeAsync()
        {
        }
        
        [Fact]
        public async Task AddBooking()
        {
            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("BookingParkingSpaceShould")))
            {
                var customer = context.Customers.Include(c => c.Vehicles).Include(c => c.Bookings).FirstOrDefault();
                var spaceId = context.ParkingSpaces.FirstOrDefault().Id;
                var vehicleId = customer.Vehicles.FirstOrDefault().Id;
                var booking = new BookingPeriodDTO { Start = DateTime.Now, End = DateTime.Now.AddDays(2), Rate = 10 };
                var command = new CreateHourlyBookingCommand(customer.IdentityId, vehicleId, spaceId, booking);
                var handler = new CreateHourlyBookingCommandHandler(
                    new CustomerRepository(context),
                    new ParkingSpaceRepository(context), 
                    new Mock<IMediator>().Object);

                var result = await handler.Handle(command);
                
                Assert.True(result.Success);
                Assert.Equal("Parking Space successfully booked", result.Message);
                
                result = await handler.Handle(command);
                
                Assert.False(result.Success);
                Assert.Equal("Parking Space not available during requested period", result.Message);
            }

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("BookingParkingSpaceShould")))
            {
                var customer = context.Customers.Include(c => c.Bookings).FirstOrDefault();
                var space = context.ParkingSpaces.FirstOrDefault();

                Assert.Single(customer.Bookings);
                Assert.Single(space.Bookings);
            }
        }

        private object BookingPeriodDTO(DateTime now, DateTime dateTime, Money money)
        {
            throw new NotImplementedException();
        }
    }
}