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
            var command = TestHelper.GetTestCreateParkingSpaceCommand("test-user");

            using (var context = new ParkMateDbContext(TestHelper.GetNamedDbContextOptions("CreateNewParkingSpace")))
            {
                var repository = new WriteRepository<ParkingSpace>(context);
                var handler = new RegisterNewParkingSpaceCommandHandler(repository);
                await handler.Handle(command, default(CancellationToken));
            }

            using (var context = new ParkMateDbContext(TestHelper.GetNamedDbContextOptions("CreateNewParkingSpace")))
            {
                Assert.Equal(1, context.ParkingSpaces.Count());
                var space = context.ParkingSpaces.Include(s => s.Availability).FirstOrDefault();

                Assert.NotNull(space.Availability);
                Assert.Equal(TestHelper.GetTestAddress(), space.Address);
                Assert.Equal(TestHelper.GetTestBookingRate(), space.BookingRate);
                Assert.Equal(TestHelper.GetTestDescription(), space.Description);
                Assert.Equal(TestHelper.GetTestAddress(), space.Address);
                Assert.Equal("test-user", space.OwnerId);
            }
        }        
        
       
    }
}