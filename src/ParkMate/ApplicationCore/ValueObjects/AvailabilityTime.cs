using System;
using System.Collections.Generic;
using ParkMate.ApplicationCore.Exceptions;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class AvailabilityTime : ValueObject
    {
        private AvailabilityTime()
        {
        }

        private AvailabilityTime(
            DayOfWeek day,
            TimeSpan availableFrom, 
            TimeSpan availableTo, 
            bool isAvailable)
        {
            DayOfWeek = day;
            AvailableFrom = availableFrom;
            AvailableTo = availableTo;
            IsAvailable = isAvailable;
        }

        public DayOfWeek DayOfWeek { get; private set; }
        public TimeSpan AvailableFrom { get; private set; }
        public TimeSpan AvailableTo { get; private set; }
        public bool IsAvailable { get; private set; }

        public static AvailabilityTime CreateAvailabilityWithHours(DayOfWeek day, TimeSpan from, TimeSpan to)
        {
            return new AvailabilityTime(day, from, to, true);
        }

        public static AvailabilityTime CreateUnavailableDay(DayOfWeek day)
        {
            return new AvailabilityTime(day, TimeSpan.Zero, TimeSpan.Zero, false);
        }

        public static AvailabilityTime Create24HourAvailability(DayOfWeek day)
        {
            return new AvailabilityTime(day, TimeSpan.Zero, TimeSpan.Zero, true);
        }

        public void FromOther(AvailabilityTime other)
        {
            AvailableFrom = other.AvailableFrom;
            AvailableTo = other.AvailableTo;
            IsAvailable = other.IsAvailable;
        }

        public bool IsAvailable24Hours()
        {
            return IsAvailable &&
                AvailableFrom == TimeSpan.Zero &&
                AvailableTo == TimeSpan.Zero;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DayOfWeek;
            yield return AvailableFrom;
            yield return AvailableTo;
            yield return IsAvailable;
        }

    }
}
