using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MediatR;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationServices.Commands;
using static ParkMate.ApplicationServices.Tests.TestHelper;

namespace ParkMate.ApplicationServices.Tests
{
    public class EditParkingSpaceAvailabilityShould
    {
        [Fact (Skip = "Requires Refactoring")] 
        public async Task ChangeToCorrectAvailability()
        {
            await CreateTestParkingSpaceInMemoryDb("ChangeToCorrectAvailability");

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("ChangeToCorrectAvailability")))
            {
                var monday = AvailabilityTime.CreateUnavailableDay(DayOfWeek.Monday);
                var tuesday = AvailabilityTime.CreateUnavailableDay(DayOfWeek.Tuesday);
                var wednesday = AvailabilityTime.CreateAvailabilityWithHours(
                    DayOfWeek.Wednesday,
                    new TimeSpan(12, 0, 0),
                    new TimeSpan(13, 0, 0));
                var thursday = AvailabilityTime.CreateAvailabilityWithHours(
                    DayOfWeek.Thursday,
                    new TimeSpan(14, 0, 0),
                    new TimeSpan(15, 0, 0));
                var friday = AvailabilityTime.CreateAvailabilityWithHours(
                    DayOfWeek.Friday,
                    new TimeSpan(16, 0, 0),
                    new TimeSpan(17, 0, 0));
                var saturday = AvailabilityTime.CreateAvailabilityWithHours(
                    DayOfWeek.Saturday,
                    new TimeSpan(18, 0, 0),
                    new TimeSpan(19, 0, 0));
                var sunday = AvailabilityTime.CreateAvailabilityWithHours(
                    DayOfWeek.Sunday,
                    new TimeSpan(20, 0, 0),
                    new TimeSpan(21, 0, 0));
                
                var times = new List<AvailabilityTime>()
                {
                    monday, tuesday, wednesday, thursday, friday, saturday, sunday
                };
                
                var repository = new ParkingSpaceRepository(context);

                var space = context.ParkingSpaces.FirstOrDefault();
                var command = new EditParkingSpaceAvailabilityCommand(space.Id, space.OwnerId, new List<DTOs.AvailableTimeDTO>());
                var handler = new EditParkingSpaceAvailabilityCommandHandler(repository, new Mock<IMediator>().Object);
                
                await handler.Handle(command);
                
                Assert.NotNull(space.Availability);
                Assert.Equal(monday, space.Availability.Monday);
                Assert.Equal(tuesday, space.Availability.Tuesday);
                Assert.Equal(wednesday, space.Availability.Wednesday);
                Assert.Equal(thursday, space.Availability.Thursday);
                Assert.Equal(friday, space.Availability.Friday);
                Assert.Equal(saturday, space.Availability.Saturday);
                Assert.Equal(sunday, space.Availability.Sunday);
            }
        }       
    }
}