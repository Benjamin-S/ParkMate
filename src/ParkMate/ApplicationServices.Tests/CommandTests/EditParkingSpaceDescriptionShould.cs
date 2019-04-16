 using System.Threading.Tasks;
 using System.Linq;
 using Xunit;
 using ParkMate.Infrastructure.Data;
 using ParkMate.ApplicationCore.ValueObjects;
 using ParkMate.ApplicationServices.Commands;
 using static ParkMate.ApplicationServices.Tests.TestHelper;
using Moq;
using MediatR;

namespace ParkMate.ApplicationServices.Tests
 {
     public class EditParkingSpaceDescriptionShould
     {
         [Fact]
         public async Task ChangeToCorrectDescription()
         {
             await CreateTestParkingSpaceInMemoryDb("EditParkingSpaceDescription");

             using (var context = new ParkMateDbContext(GetNamedDbContextOptions("EditParkingSpaceDescription")))
             {
                 var space = context.ParkingSpaces.FirstOrDefault();
                 var repository = new ParkingSpaceRepository(context);
                 var description = new ParkingSpaceDescription("New Test Title", "New Description", "newfile.jpg");
                 var command = new EditParkingSpaceDescriptionCommand(space.Id, space.OwnerId, description);
                 var handler = new EditParkingSpaceDescriptionCommandHandler(repository, new Mock<IMediator>().Object);

                 await handler.Handle(command);

                 Assert.NotNull(space.BookingRate);
                 Assert.NotEqual(GetTestDescription(), space.Description);
                 Assert.Equal(description, space.Description);
             }
         }
     }
 }
