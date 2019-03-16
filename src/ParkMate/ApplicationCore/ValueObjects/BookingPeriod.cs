using System;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class BookingPeriod
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public BookingPeriod(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        public static BookingPeriod CreateOneHourPeriod(DateTime day)
        {
            throw new NotImplementedException();
        }
        
        public static BookingPeriod CreateOneDayPeriod(DateTime day)
        {
            throw new NotImplementedException();
        }

        public static BookingPeriod CreateOneWeekPeriod(DateTime startDateTime)
        {
            throw new NotImplementedException();
        }
        public static BookingPeriod CreateOneMonthPeriod(DateTime startDateTime)
        {
            throw new NotImplementedException();
        }
        public bool Overlaps(BookingPeriod dateTimeRange)
        {
            throw new NotImplementedException();
        }
    }
}