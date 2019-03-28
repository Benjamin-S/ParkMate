using System;
using System.Collections.Generic;
using ParkMate.ApplicationCore.ValueObjects;

namespace ParkMate.ApplicationCore.Entities
{
    public class SpaceAvailability : BaseEntity
    {
        private SpaceAvailability()
        {
        }

        public SpaceAvailability(
            bool isVisible,
            AvailabilityTime monday,
            AvailabilityTime tuesday,
            AvailabilityTime wednesday,
            AvailabilityTime thursday,
            AvailabilityTime friday,
            AvailabilityTime saturday,
            AvailabilityTime sunday)
        {
            IsVisible = isVisible;
            _availabilityOnDay[DayOfWeek.Monday] = Monday = monday ??
                throw new ArgumentNullException(nameof(monday)); 

            _availabilityOnDay[DayOfWeek.Tuesday] = Tuesday = tuesday ??
                throw new ArgumentNullException(nameof(tuesday));

            _availabilityOnDay[DayOfWeek.Wednesday] = Wednesday = wednesday ??
                throw new ArgumentNullException(nameof(wednesday));

            _availabilityOnDay[DayOfWeek.Thursday] = Thursday = thursday ??
                throw new ArgumentNullException(nameof(thursday));

            _availabilityOnDay[DayOfWeek.Friday] = Friday = friday ??
                throw new ArgumentNullException(nameof(friday));

            _availabilityOnDay[DayOfWeek.Saturday] = Saturday = saturday ??
                throw new ArgumentNullException(nameof(saturday));

            _availabilityOnDay[DayOfWeek.Sunday] = Sunday = sunday ??
                throw new ArgumentNullException(nameof(sunday));
        }

        public bool IsVisible { get; private set; }
        public AvailabilityTime Monday { get; private set; }
        public AvailabilityTime Tuesday { get; private set; }
        public AvailabilityTime Wednesday { get; private set; }
        public AvailabilityTime Thursday { get; private set; }
        public AvailabilityTime Friday { get; private set; }
        public AvailabilityTime Saturday { get; private set; }
        public AvailabilityTime Sunday { get; private set; }

        private Dictionary<DayOfWeek, AvailabilityTime> _availabilityOnDay { get; }
             = new Dictionary<DayOfWeek, AvailabilityTime>();

        public static SpaceAvailability Create247Availability()
        {
            return new SpaceAvailability(
                true,
                AvailabilityTime.Create24HourAvailability(),
                AvailabilityTime.Create24HourAvailability(),
                AvailabilityTime.Create24HourAvailability(),
                AvailabilityTime.Create24HourAvailability(),
                AvailabilityTime.Create24HourAvailability(),
                AvailabilityTime.Create24HourAvailability(),
                AvailabilityTime.Create24HourAvailability()
            );
        }
        public void SetAvailabilityForDay(DayOfWeek day, AvailabilityTime availability)
        {
            _availabilityOnDay[day] = availability ??
                throw new ArgumentNullException(nameof(availability));
        }
    }
}
