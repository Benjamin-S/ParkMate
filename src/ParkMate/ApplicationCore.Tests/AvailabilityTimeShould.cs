using Xunit;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationCore.Entities;
using ParkMate.ApplicationCore.Exceptions;

using System;

namespace ApplicationCore.Tests
{
    public class AvailabilityTimeShould
    {
        [Fact]
        public void ThrowExceptionIfLessThanOneHour()
        {
            var start = new TimeSpan(1, 0, 0);
            var end = new TimeSpan(1, 59, 0);

            Assert.Throws<InvalidAvailabilityTimeException>(() => 
                AvailabilityTime.CreateAvailabilityWithHours(DayOfWeek.Monday, start, end));
        }

        [Fact]
        public void NotThrowExceptionIfEqualOrMoreThanOneHour()
        {
            var start = new TimeSpan(1, 0, 0);
            var end = new TimeSpan(2, 0, 0);

            _ = AvailabilityTime.CreateAvailabilityWithHours(DayOfWeek.Monday,start, end);
        }

        [Fact]
        public void EqualDifferentObjectWithSameValue()
        {
            var time1 = AvailabilityTime.CreateAvailabilityWithHours(DayOfWeek.Monday,new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));
            var time2 = AvailabilityTime.CreateAvailabilityWithHours(DayOfWeek.Monday,new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));

            Assert.Equal(time1, time2);
        }

        [Fact]
        public void NotEqualDifferentObjectWithDifferentValue()
        {
            var time1 = AvailabilityTime.CreateAvailabilityWithHours(DayOfWeek.Monday,new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));
            var time2 = AvailabilityTime.CreateAvailabilityWithHours(DayOfWeek.Monday,new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 1));

            Assert.NotEqual(time1, time2);
        }

        [Fact]
        public void ReturnTrueOnIsAvailable24HoursIfIs24Hours()
        {
            var availability = AvailabilityTime.Create24HourAvailability(DayOfWeek.Monday);

            Assert.True(availability.IsAvailable24Hours());
        }

        [Fact]
        public void ReturnFalseOnIsAvailable24HoursIfNot24Hours()
        {
            var availability = AvailabilityTime.CreateAvailabilityWithHours(DayOfWeek.Monday,new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));

            Assert.False(availability.IsAvailable24Hours());
        }

        [Fact]
        public void ReturnTrueOnIsAvailableIfAvailable()
        {
            var availability = AvailabilityTime.CreateAvailabilityWithHours(DayOfWeek.Monday,new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));

            Assert.True(availability.IsAvailable);
        }

        [Fact]
        public void ReturnFalseOnIsAvailableIfNotAvailable()
        {
            var availability = AvailabilityTime.CreateUnavailableDay(DayOfWeek.Monday);

            Assert.False(availability.IsAvailable);
        }
    }
}
