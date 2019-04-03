using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkMate.ApplicationCore.Entities;
using ParkMate.Infrastructure.Data;
using Xunit;
using static ParkMate.ApplicationServices.Tests.TestHelper;
using ParkMate.ApplicationServices.Queries;

namespace ParkMate.ApplicationServices.Tests
{
    public class GetSingleParkingSpaceQueryShould
    {
        public GetSingleParkingSpaceQueryShould()
        {
            CreateTestParkingSpaceInDb("GetSingleParkingSpaceQuery", "user1");
            CreateTestParkingSpaceInDb("GetSingleParkingSpaceQuery", "user2");
            CreateTestParkingSpaceInDb("GetSingleParkingSpaceQuery", "user3");
        }
        
        [Fact]
        public async Task GetCorrectResult()
        {
            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("GetSingleParkingSpaceQuery")))
            {
                int id = context.ParkingSpaces.Take(1).SingleOrDefault().Id;
                var command = new GetSingleParkingSpaceQuery(id);
                var handler = new GetSingleParkingSpaceQueryHandler(context);
                
                var result = await handler.Handle(command);

                Assert.True(result.Success);
                Assert.IsType<ParkingSpace>(result.PayLoad);
            }
        }

        [Fact]
        public async Task ReturnErrorIfNotFound()
        {
            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("GetSingleParkingSpaceQuery")))
            {
                var command = new GetSingleParkingSpaceQuery(999);
                var handler = new GetSingleParkingSpaceQueryHandler(context);
                
                var result = await handler.Handle(command);

                Assert.False(result.Success);
                Assert.Equal("Parking space not found", result.Message);
            }
        }
    }
}