using System;
using System.Collections.Generic;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class BookingRate : ValueObject
    {
        private BookingRate()
        {
        }
        
        public BookingRate(Money hourlyRate, Money dailyRate)
        {
            HourlyRate = hourlyRate ?? throw new ArgumentNullException(nameof(hourlyRate));
            DailyRate = dailyRate ?? throw new ArgumentNullException(nameof(dailyRate));
        }

        public Money HourlyRate { get; private set; }
        public Money DailyRate { get; private set; }

        public void UpdateFrom(BookingRate other)
        {
            HourlyRate.UpdateFrom(other.HourlyRate);
            DailyRate.UpdateFrom(other.DailyRate);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HourlyRate;
            yield return DailyRate;
        }
    }
}
