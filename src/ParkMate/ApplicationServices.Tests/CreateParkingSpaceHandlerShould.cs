using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkMate.Infrastructure.Data;
using Xunit;
using static ApplicationServices.Tests.TestHelper;

namespace ApplicationServices.Tests
{
    public class CreateParkingSpaceHandlerShould
    {
        [Fact]
        public async Task CreateNewParkingSpace()
        {
            await CreateTestParkingSpaceInDb("CreateNewParkingSpace");

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("CreateNewParkingSpace")))
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
        
       
    }
}