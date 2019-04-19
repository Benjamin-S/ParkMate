using System.Threading.Tasks;
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
    public class EditParkingSpaceBookingRateShould
    {
        [Fact]
        public async Task ChangeParkingSpaceToCorrectBookingRate()
        {

            await CreateTestParkingSpaceInMemoryDb("EditParkingSpaceBookingRate");

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("EditParkingSpaceBookingRate")))
            {
                var space = context.ParkingSpaces.FirstOrDefault();
                var repository = new ParkingSpaceRepository(context);
                var bookingRate = new BookingRate(new Money(12), new Money(34));
                var bookingRateDto = new BookingRateDTO { HourlyRate = 12, DailyRate = 34 };
                var command = new EditParkingSpaceBookingRateCommand(space.Id, space.OwnerId, bookingRateDto);
                var handler = new EditParkingSpaceBookingRateCommandHandler(repository, new Mock<IMediator>().Object);
                
                await handler.Handle(command);
                
                Assert.NotNull(space.BookingRate);
                Assert.Equal(bookingRate, space.BookingRate);
            }
        }       
    }
}
