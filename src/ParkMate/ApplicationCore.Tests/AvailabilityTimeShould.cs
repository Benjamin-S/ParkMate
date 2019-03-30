using Xunit;
using ParkMate.ApplicationCore.ValueObjects;
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
                AvailabilityTime.CreateAvailabilityWithHours(start, end));
        }

        [Fact]
        public void NotThrowExceptionIfEqualOrMoreThanOneHour()
        {
            var start = new TimeSpan(1, 0, 0);
            var end = new TimeSpan(2, 0, 0);

            _ = AvailabilityTime.CreateAvailabilityWithHours(start, end);
        }

        [Fact]
        public void EqualDifferentObjectWithSameValue()
        {
            var time1 = AvailabilityTime.CreateAvailabilityWithHours(new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));
            var time2 = AvailabilityTime.CreateAvailabilityWithHours(new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));

            Assert.Equal(time1, time2);
        }

        [Fact]
        public void NotEqualDifferentObjectWithDifferentValue()
        {
            var time1 = AvailabilityTime.CreateAvailabilityWithHours(new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));
            var time2 = AvailabilityTime.CreateAvailabilityWithHours(new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 1));

            Assert.NotEqual(time1, time2);
        }

        [Fact]
        public void ReturnTrueOnIsAvailable24HoursIfIs24Hours()
        {
            var availability = AvailabilityTime.Create24HourAvailability();

            Assert.True(availability.IsAvailable24Hours());
        }

        [Fact]
        public void ReturnFalseOnIsAvailable24HoursIfNot24Hours()
        {
            var availability = AvailabilityTime.CreateAvailabilityWithHours(new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));

            Assert.False(availability.IsAvailable24Hours());
        }

        [Fact]
        public void ReturnTrueOnIsAvailableIfAvailable()
        {
            var availability = AvailabilityTime.CreateAvailabilityWithHours(new TimeSpan(1, 0, 0), new TimeSpan(2, 0, 0));

            Assert.True(availability.IsAvailable);
        }

        [Fact]
        public void ReturnFalseOnIsAvailableIfNotAvailable()
        {
            var availability = AvailabilityTime.CreateUnavailableDay();

            Assert.False(availability.IsAvailable);
        }
    }
}
