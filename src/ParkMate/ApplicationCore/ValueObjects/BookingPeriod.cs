using System;
using System.Collections.Generic;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class BookingPeriod : ValueObject
    {
        private BookingPeriod()
        {
        }

        public BookingPeriod(DateTime start, DateTime end, Money charge)
        {
            if (end <= start)
            {
                throw new ArgumentException("End time cannot be before or equal to start time");
            }
            Start = start;
            End = end;
            Charge = charge;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public Money Charge { get; private set; }

        public static BookingPeriod CreateHourlyBooking(DateTime start, DateTime end, BookingRate rate)
        {
            var hours = end.Subtract(start).Hours;
            var charge = hours * rate.HourlyRate;
            return new BookingPeriod(start, end, charge);
        }

        public static BookingPeriod CreateDailyBooking(DateTime start, DateTime end, BookingRate rate)
        {
            var days = end.Subtract(start).Days;
            var charge = days * rate.DailyRate;
            return new BookingPeriod(start, end, charge);
        }

        public bool Overlaps(BookingPeriod dateTimeRange)
        {
            return this.Start < dateTimeRange.End &&
                   this.End > dateTimeRange.Start;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
            yield return Charge;
        }
    }
}
