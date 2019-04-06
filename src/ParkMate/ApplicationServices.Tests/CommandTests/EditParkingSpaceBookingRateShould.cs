using System.Threading.Tasks;
using System.Linq;
using Xunit;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Commands;
using static ParkMate.ApplicationServices.Tests.TestHelper;

namespace ParkMate.ApplicationServices.Tests
{
    public class EditParkingSpaceBookingRateShould
    {
        [Fact]
        public async Task ChangeParkingSpaceToCorrectBookingRate()
        {

            await CreateTestParkingSpaceInDb("EditParkingSpaceBookingRate");

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("EditParkingSpaceBookingRate")))
            {
                var space = context.ParkingSpaces.FirstOrDefault();
                var repository = new ParkingSpaceRepository(context);
                var bookingRate = new BookingRate(new Money(12), new Money(34));
                var command = new EditParkingSpaceBookingRateCommand(space.Id, bookingRate);
                var handler = new EditParkingSpaceBookingRateCommandHandler(repository);
                
                await handler.Handle(command);
                
                Assert.NotNull(space.BookingRate);
                Assert.NotEqual(GetTestBookingRate(), space.BookingRate);
                Assert.Equal(bookingRate, space.BookingRate);
            }
        }       
    }
}
