using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParkMate.Infrastructure.Data;
using ParkMate.ApplicationCore.ValueObjects;
using Xunit;
using ApplicationServices.Commands;
using static ApplicationServices.Tests.TestHelper;

namespace ApplicationServices.Tests
{
    public class EditParkingSpaceAvailabilityShould
    {
        [Fact]
        public async Task ChangeToCorrectAvailability()
        {

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("ChangePToCorrectAvailability")))
            {
                var command = GetTestCreateParkingSpaceCommand("test-user");
                var repository = new ParkingSpaceRepository(context);
                var handler = new RegisterNewParkingSpaceCommandHandler(repository);
                await handler.Handle(command);
            }

            using (var context = new ParkMateDbContext(GetNamedDbContextOptions("ChangePToCorrectAvailability")))
            {
                var repository = new ParkingSpaceRepository(context);
                var space = await repository.GetByIdAsync(1);
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
                var command = new EditParkingSpaceAvailabilityCommand(space.Id, times);
                var handler = new EditParkingSpaceAvailabilityCommandHandler(repository);
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