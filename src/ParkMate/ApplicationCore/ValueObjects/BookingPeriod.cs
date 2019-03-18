using System;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class BookingPeriod
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public BookingPeriod(DateTime start, DateTime end)
            if (end <= start)
            {
                throw  new ArgumentException("End time cannot be before or equal to start time");
            }
            Start = start;    
            End = end;
            Start = start;
            End = end;

            if (start == end)
                throw new ArgumentException("Start date and end date are equal");

            if(end < start)
                throw new ArgumentException("End date precedes start date");
        }
        public static BookingPeriod CreateOneHourPeriod(DateTime day)
        {
            var start = day;
            var end = day.AddHours(1);

            return(new BookingPeriod(start, end));
        }
        
        public static BookingPeriod CreateOneDayPeriod(DateTime day)
        {
            var start = day;
            var end = day.AddDays(1);

            return(new BookingPeriod(start, end));
        }

        public static BookingPeriod CreateOneWeekPeriod(DateTime startDateTime)
        {
            var start = startDateTime;
            var end = startDateTime.AddDays(7);

            return(new BookingPeriod(start, end));
        }
        public static BookingPeriod CreateOneMonthPeriod(DateTime startDateTime)
        {
            var start = startDateTime;
            var end = startDateTime.AddMonths(1);

            return(new BookingPeriod(start, end));
        }
        public bool Overlaps(BookingPeriod dateTimeRange)
        {
            return (this.Start < dateTimeRange.End && dateTimeRange.Start < this.End);
        }
    }
}
