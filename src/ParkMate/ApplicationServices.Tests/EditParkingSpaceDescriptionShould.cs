 using System.Threading.Tasks;
 using System.Linq;
 using Xunit;
 using ParkMate.Infrastructure.Data;
 using ParkMate.ApplicationCore.ValueObjects;
 using ApplicationServices.Commands;
 using static ApplicationServices.Tests.TestHelper;

 namespace ApplicationServices.Tests
 {
     public class EditParkingSpaceDescriptionShould
     {
         [Fact]
         public async Task ChangeToCorrectDescription()
         {
             await CreateTestParkingSpaceInDb("EditParkingSpaceDescription");

             using (var context = new ParkMateDbContext(GetNamedDbContextOptions("EditParkingSpaceDescription")))
             {
                 var space = context.ParkingSpaces.FirstOrDefault();
                 var repository = new ParkingSpaceRepository(context);
                 var description = new ParkingSpaceDescription("New Test Title", "New Description", "newfile.jpg");
                 var command = new EditParkingSpaceDescriptionCommand(space.Id, description);
                 var handler = new EditParkingSpaceDescriptionCommandHandler(repository);

                 await handler.Handle(command);

                 Assert.NotNull(space.BookingRate);
                 Assert.NotEqual(GetTestDescription(), space.Description);
                 Assert.Equal(description, space.Description);
             }
         }
     }
 }
