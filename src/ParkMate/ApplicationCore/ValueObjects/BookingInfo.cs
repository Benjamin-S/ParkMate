using System;
using System.Collections.Generic;
using ApplicationCore.Enums;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class BookingInfo : ValueObject
    {
        private BookingInfo()
        {
        }

        private BookingInfo(
            DateTime start, 
            DateTime end, 
            Money total,
            Money rate,
            BillingUnit billingUnit,
            int bookingUnits)
        {
            if (end <= start)
            {
                throw new ArgumentException("End time cannot be before or equal to start time");
            }
            Start = start;
            End = end;
            Total = total;
            Rate = rate;
            BillingUnit = billingUnit;
            BookingUnits = bookingUnits;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public Money Total { get; private set; }
        public Money Rate { get; private set; }
        public BillingUnit BillingUnit { get; private set; }
        public int BookingUnits { get; private set; }

        public static BookingInfo CreateHourlyBooking(DateTime start, DateTime end, Money rate)
        {
            var hours = end.Subtract(start).Hours;
            var total = hours * rate;
            return new BookingInfo(start, end, total, rate, BillingUnit.Hourly, hours);
        }

        public static BookingInfo CreateDailyBooking(DateTime start, DateTime end, Money rate)
        {
            var days = end.Subtract(start).Days;
            var total = days * rate;
            return new BookingInfo(start, end, total, rate, BillingUnit.Daily, days);
        }

        public bool Overlaps(BookingInfo dateTimeRange)
        {
            return this.Start < dateTimeRange.End &&
                   this.End > dateTimeRange.Start;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
            yield return Total;
        }
    }
}
