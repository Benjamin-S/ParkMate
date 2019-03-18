using System;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class BookingPeriod
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public BookingPeriod(DateTime start, DateTime end)
        {
            if (end <= start)
            {
                throw  new ArgumentException("End time cannot be before or equal to start time");
            }
            Start = start;    
            End = end;
        }
        public static BookingPeriod CreateOneHourPeriod(DateTime day)
        {
            return new BookingPeriod(day, day.AddHours(1));
        }
        
        public static BookingPeriod CreateOneDayPeriod(DateTime day)
        {
            return new BookingPeriod(day, day.AddDays(1));
        }

        public static BookingPeriod CreateOneWeekPeriod(DateTime startDateTime)
        {
            return new BookingPeriod(startDateTime, startDateTime.AddDays(7));
        }
        public static BookingPeriod CreateOneMonthPeriod(DateTime startDateTime)
        {
            return new BookingPeriod(startDateTime, startDateTime.AddMonths(1));
        }
        public bool Overlaps(BookingPeriod dateTimeRange)
        {
            return this.Start < dateTimeRange.End && 
                   this.End > dateTimeRange.Start;
        }
    }
}
