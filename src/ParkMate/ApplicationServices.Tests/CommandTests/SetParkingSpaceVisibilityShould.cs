using System.Threading.Tasks;
using System.Linq;
using Xunit;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationServices.Commands;
using Microsoft.EntityFrameworkCore;
using static ParkMate.ApplicationServices.Tests.TestHelper;
using Moq;
using MediatR;

namespace ParkMate.ApplicationServices.Tests
{
    public class SetParkingSpaceVisibilityShould
    {
        [Fact]
        public async Task ChangeUnlistedSpaceToListed()
        {
            await CreateTestParkingSpaceInMemoryDb("SetParkingSpaceVisibilityShould");

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("SetParkingSpaceVisibilityShould")))
            {
                var space = context.ParkingSpaces.Include(s => s.Availability).FirstOrDefault();
                var repository = new ParkingSpaceRepository(context);
                var command = new SetParkingSpaceVisibilityCommand(space.Id, space.OwnerId, true);
                var handler = new SetParkingSpaceVisibilityCommandHandler(repository, new Mock<IMediator>().Object);
                bool previousState = space.Availability.IsVisible;
                
                await handler.Handle(command);

                Assert.NotEqual(previousState, space.Availability.IsVisible);
                Assert.True(space.Availability.IsVisible);
            }
        }
    }
}
