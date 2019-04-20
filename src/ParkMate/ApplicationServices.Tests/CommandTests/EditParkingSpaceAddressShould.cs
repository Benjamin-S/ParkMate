using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Xunit;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Commands;
using static ParkMate.ApplicationServices.Tests.TestHelper;
using Moq;
using MediatR;
using ParkMate.ApplicationServices.DTOs;

namespace ParkMate.ApplicationServices.Tests
{
    public class EditParkingSpaceAddressShould
    {
        [Fact]
        public async Task ChangeParkingSpaceToCorrectAddress()
        {
            await CreateTestParkingSpaceInMemoryDb("EditParkingSpaceAddress");

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("EditParkingSpaceAddress")))
            {
                var space = context.ParkingSpaces.FirstOrDefault();
                var repository = new ParkingSpaceRepository(context);
                var addressDto = new AddressDTO
                {
                    Street = "123 Test Street",
                    City = "TestVille",
                    State = "ABC",
                    Zip = "12345",
                    Latitude = 1.2d,
                    Longitude = 3.4
                };
                var address = new Address(addressDto.Street, addressDto.City, addressDto.State, 
                    addressDto.Zip, new Point(addressDto.Latitude, addressDto.Longitude));

                var command = new EditParkingSpaceAddressCommand(space.Id, space.OwnerId, addressDto);
                var handler = new EditParkingSpaceAddressCommandHandler(repository, new Mock<IMediator>().Object);
               
                await handler.Handle(command);
                             
                Assert.NotNull(space.Address);
                Assert.Equal(address, space.Address);
            }
        }       
    }
}
