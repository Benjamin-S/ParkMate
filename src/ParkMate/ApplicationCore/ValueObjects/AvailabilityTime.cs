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
            TimeSpan availableFrom, 
            TimeSpan availableTo, 
            bool isAvailable)
        {
            AvailableFrom = availableFrom;
            AvailableTo = availableTo;
            IsAvailable = isAvailable;
        }

        public TimeSpan AvailableFrom { get; private set; }
        public TimeSpan AvailableTo { get; private set; }
        public bool IsAvailable { get; private set; }

        public static AvailabilityTime CreateAvailabilityWithHours(TimeSpan from, TimeSpan to)
        {
            if (from.Subtract(to).Duration().Hours < 1)
            {
                throw new InvalidAvailabilityTimeException(from, to, "Minimum availability period is 1 hour");
            }
            return new AvailabilityTime(from, to, true);
        }

        public static AvailabilityTime CreateUnavailableDay()
        {
            return new AvailabilityTime(TimeSpan.Zero, TimeSpan.Zero, false);
        }

        public static AvailabilityTime Create24HourAvailability()
        {
            return new AvailabilityTime(TimeSpan.Zero, TimeSpan.Zero, true);
        }

        public bool IsAvailable24Hours()
        {
            return IsAvailable &&
                AvailableFrom == TimeSpan.Zero &&
                AvailableTo == TimeSpan.Zero;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return AvailableFrom;
            yield return AvailableTo;
            yield return IsAvailable;
        }
    }
}
