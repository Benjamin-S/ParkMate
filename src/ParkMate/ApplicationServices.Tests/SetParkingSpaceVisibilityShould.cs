using System.Threading.Tasks;
using System.Linq;
using Xunit;
using ParkMate.Infrastructure.Data;
using ApplicationServices.Commands;
using Microsoft.EntityFrameworkCore;
using static ApplicationServices.Tests.TestHelper;

namespace ApplicationServices.Tests
{
    public class SetParkingSpaceVisibilityShould
    {
        [Fact]
        public async Task ChangeUnlistedSpaceToListed()
        {
            await CreateTestParkingSpaceInDb("SetParkingSpaceVisibilityShould");

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("SetParkingSpaceVisibilityShould")))
            {
                var space = context.ParkingSpaces.Include(s => s.Availability).FirstOrDefault();
                var repository = new ParkingSpaceRepository(context);
                var command = new SetParkingSpaceVisibilityCommand(space.Id, true);
                var handler = new SetParkingSpaceVisibilityCommandCommandHandler(repository);
                bool previousState = space.Availability.IsVisible;
                
                await handler.Handle(command);

                Assert.NotEqual(previousState, space.Availability.IsVisible);
                Assert.True(space.Availability.IsVisible);
            }
        }
    }
}
