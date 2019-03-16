using System;
using Xunit;
using ParkMate.ApplicationCore.ValueObjects; 

namespace ApplicationCore.Tests
{
    public class BookingPeriodShould
    {
        [Fact]
        public void ThrowExceptionIfEndPrecedesStart()
        {
            var start = new DateTime(2019, 3, 2);
            var end = new DateTime(2019, 3, 1);

            Assert.Throws<ArgumentException>(() => new BookingPeriod(start, end));
        }
        
        [Fact]
        public void ThrowExceptionIfStartEqualsEnd()
        {
            var start = new DateTime(2019, 3, 1);
            var end = new DateTime(2019, 3, 1);

            Assert.Throws<ArgumentException>(() => new BookingPeriod(start, end));
        }
        
        [Fact]
        public void CreateOneHourPeriod()
        {
            var start = new DateTime(2019, 3, 1, 13, 30, 0);
            var end = start.AddHours(1);
            
            var sut = BookingPeriod.CreateOneHourPeriod(start);
            
            Assert.Equal(sut.End.Subtract(sut.Start), TimeSpan.FromHours(1));
        }
        
        [Fact]
        public void CreateOneDayPeriod()
        {
            var start = new DateTime(2019, 3, 1, 13, 30, 0);
            var end = start.AddDays(1);
            
            var sut = BookingPeriod.CreateOneDayPeriod(start);
            
            Assert.Equal(sut.End.Subtract(sut.Start), TimeSpan.FromDays(1));
        }
        
        [Fact]
        public void CreateOneWeekPeriod()
        {
            var start = new DateTime(2019, 3, 1, 13, 30, 0);
            var end = start.AddDays(7);
            
            var sut = BookingPeriod.CreateOneWeekPeriod(start);
            
            Assert.Equal(sut.End.Subtract(sut.Start), TimeSpan.FromDays(7));
        }
        
        [Fact]
        public void CreateOneMonthPeriod()
        {
            var start = new DateTime(2019, 3, 1, 13, 30, 0);
            var end = start.AddMonths(1);
            
            var sut = BookingPeriod.CreateOneMonthPeriod(start);
            
            Assert.Equal(sut.End.Subtract(sut.Start), end.Subtract(start));
        }
        
        [Fact]
        public void ReturnTrueIfPeriodsOverlap()
        {
            var period1 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7));
            var period2 = new BookingPeriod(new DateTime(2019, 3, 6), new DateTime(2019, 3, 8));
            
            var period3 = new BookingPeriod(new DateTime(2019, 3, 6), new DateTime(2019, 3, 14));
            var period4 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7));
            
            var period5 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7));
            var period6 = new BookingPeriod(new DateTime(2019, 3, 3), new DateTime(2019, 3, 4));
            
            Assert.True(period1.Overlaps(period2));
            Assert.True(period2.Overlaps(period1));
            
            Assert.True(period3.Overlaps(period4));
            Assert.True(period4.Overlaps(period3));
            
            Assert.True(period5.Overlaps(period6));
            Assert.True(period6.Overlaps(period5));
        }
        
        [Fact]
        public void ReturnFalseIfPeriodsDontOverlap()
        {
            var period1 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7));
            var period2 = new BookingPeriod(new DateTime(2019, 3, 8), new DateTime(2019, 3, 9));
            
            var period3 = new BookingPeriod(new DateTime(2019, 3, 6), new DateTime(2019, 3, 14));
            var period4 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 5));
            
            Assert.False(period1.Overlaps(period2));
            Assert.False(period2.Overlaps(period1));
            
            Assert.False(period3.Overlaps(period4));
            Assert.False(period4.Overlaps(period3));
        }
    }
}