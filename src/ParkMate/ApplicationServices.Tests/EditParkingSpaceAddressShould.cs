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
    public class EditParkingSpaceAddressShould
    {
        [Fact]
        public async Task ChangeParkingSpaceToCorrectAddress()
        {

            using (var context = new ParkMateDbContext(TestHelper.GetNamedDbContextOptions("EditParkingSpaceAddress")))
            {
                var command = TestHelper.GetTestCreateParkingSpaceCommand("test-user");
                var repository = new WriteRepository<ParkingSpace>(context);
                var handler = new RegisterNewParkingSpaceCommandHandler(repository);
                await handler.Handle(command, default(CancellationToken));
            }

            using (var context = new ParkMateDbContext(TestHelper.GetNamedDbContextOptions("EditParkingSpaceAddress")))
            {
                var space = context.ParkingSpaces.FirstOrDefault();
                var repository = new WriteRepository<ParkingSpace>(context);
                var address = new Address("567 Test Road", "TestVille", "Tst", "56789", new Point(9,10));
                var command = new EditParkingSpaceAddressCommand(space.Id, address);
                var handler = new EditParkingSpaceAddressCommandHandler(repository);
                await handler.Handle(command);
                
                
                Assert.NotNull(space.Address);
                Assert.NotEqual(TestHelper.GetTestAddress(), space.Address);
                Assert.Equal(address, space.Address);
                
            }
        }       
    }

    
}
