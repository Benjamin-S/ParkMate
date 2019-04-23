using System;
using System.Collections.Generic;
using System.Linq;
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
            Monday = monday ?? throw new ArgumentNullException(nameof(monday));
            Tuesday = tuesday ?? throw new ArgumentNullException(nameof(tuesday));
            Wednesday = wednesday ?? throw new ArgumentNullException(nameof(wednesday));
            Thursday = thursday ?? throw new ArgumentNullException(nameof(thursday));
            Friday = friday ?? throw new ArgumentNullException(nameof(friday));
            Saturday = saturday ?? throw new ArgumentNullException(nameof(saturday));
            Sunday = sunday ?? throw new ArgumentNullException(nameof(sunday));
        }

        public bool IsVisible { get; private set; }
        public AvailabilityTime Monday { get; private set; }
        public AvailabilityTime Tuesday { get; private set; }
        public AvailabilityTime Wednesday { get; private set; }
        public AvailabilityTime Thursday { get; private set; }
        public AvailabilityTime Friday { get; private set; }
        public AvailabilityTime Saturday { get; private set; }
        public AvailabilityTime Sunday { get; private set; }

        public static SpaceAvailability Create247Availability()
        {
            return new SpaceAvailability(
                false,
                AvailabilityTime.Create24HourAvailability(DayOfWeek.Monday),
                AvailabilityTime.Create24HourAvailability(DayOfWeek.Tuesday),
                AvailabilityTime.Create24HourAvailability(DayOfWeek.Wednesday),
                AvailabilityTime.Create24HourAvailability(DayOfWeek.Thursday),
                AvailabilityTime.Create24HourAvailability(DayOfWeek.Friday),
                AvailabilityTime.Create24HourAvailability(DayOfWeek.Saturday),
                AvailabilityTime.Create24HourAvailability(DayOfWeek.Sunday)
            );
        }

        internal void SetVisible(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public bool IsAvailable(BookingInfo period)
        {
            var day1 = period.Start;
            var day2 = period.End;
            var days = new List<DayOfWeek> { day1.DayOfWeek };

            while (day1.Day != day2.Day)
            {
                day1 = day1.AddDays(1);
                days.Add(day1.DayOfWeek);
            }
            return days.All(d => GetAvailabilityForDay(d).IsAvailable);
        }

        public void SetAvailabilityForDay(AvailabilityTime availability)
        {
            switch (availability.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    Monday = availability;
                    break;
                case DayOfWeek.Tuesday:
                    Tuesday = availability;
                    break;
                case DayOfWeek.Wednesday:
                    Wednesday = availability;
                    break;
                case DayOfWeek.Thursday:
                    Thursday = availability;
                    break;
                case DayOfWeek.Friday:
                    Friday = availability;
                    break;
                case DayOfWeek.Saturday:
                    Saturday = availability;
                    break;
                case DayOfWeek.Sunday:
                    Sunday = availability;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public AvailabilityTime GetAvailabilityForDay(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return Monday;
                case DayOfWeek.Tuesday:
                    return Tuesday;
                case DayOfWeek.Wednesday:
                    return Wednesday;
                case DayOfWeek.Thursday:
                    return Thursday;
                case DayOfWeek.Friday:
                    return Friday;
                case DayOfWeek.Saturday:
                    return Saturday;
                case DayOfWeek.Sunday:
                    return Sunday;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
