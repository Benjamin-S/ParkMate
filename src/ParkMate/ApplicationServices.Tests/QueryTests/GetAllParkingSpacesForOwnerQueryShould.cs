using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkMate.Infrastructure.Data;
using Xunit;
using static ParkMate.ApplicationServices.Tests.TestHelper;
using ParkMate.ApplicationServices.Queries;

namespace ParkMate.ApplicationServices.Tests
{
    public class GetAllParkingSpacesForOwnerQueryShould
    {
        public GetAllParkingSpacesForOwnerQueryShould()
        {
            CreateTestParkingSpaceInDb("GetCorrectNumberOfResults", "user1");
            CreateTestParkingSpaceInDb("GetCorrectNumberOfResults", "user1");
            CreateTestParkingSpaceInDb("GetCorrectNumberOfResults", "user1");
            CreateTestParkingSpaceInDb("GetCorrectNumberOfResults", "user2");
            CreateTestParkingSpaceInDb("GetCorrectNumberOfResults", "user3");
        }
        [Fact]
        public async Task GetCorrectNumberOfResults()
        {
            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("GetCorrectNumberOfResults")))
            {
                var command = new GetAllParkingSpacesForOwnerQuery("user1");
                var handler = new GetAllParkingSpacesForOwnerQueryHandler(context);
                
                var result = await handler.Handle(command);

                Assert.True(result.Success);
                Assert.Equal(3, result.PayLoad.Count);
            }
        }

        [Fact]
        public async Task ReturnErrorIfNotFound()
        {
            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("GetCorrectNumberOfResults")))
            {
                var command = new GetAllParkingSpacesForOwnerQuery("Non existing user");
                var handler = new GetAllParkingSpacesForOwnerQueryHandler(context);
                
                var result = await handler.Handle(command);

                Assert.False(result.Success);
                Assert.Equal("No parking spaces found for user", result.Message);
            }
        }
    }
}