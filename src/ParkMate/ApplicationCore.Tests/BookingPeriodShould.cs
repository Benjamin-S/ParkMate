using System;
using Xunit;
using ParkMate.ApplicationCore.ValueObjects; 

namespace ApplicationCore.Tests
{
    public class BookingPeriodShould
    {
        Money money = new Money();

        [Fact]
        public void ThrowExceptionIfEndPrecedesStart()
        {
            var start = new DateTime(2019, 3, 2);
            var end = new DateTime(2019, 3, 1);

            Assert.Throws<ArgumentException>(() => new BookingPeriod(start, end, money));
        }
        
        [Fact]
        public void ThrowExceptionIfStartEqualsEnd()
        {
            var start = new DateTime(2019, 3, 1);
            var end = new DateTime(2019, 3, 1);

            Assert.Throws<ArgumentException>(() => new BookingPeriod(start, end, money));
        }

        [Fact]
        public void CreateHourlyPeriodWithCorrectAmount()
        {
            var start = new DateTime(2019, 3, 1, 12, 0, 0);
            var end = new DateTime(2019, 3, 1, 16, 0, 0);
            var rate = new BookingRate(new Money(5), new Money(10));

            var sut = BookingPeriod.CreateHourlyBooking(start, end, rate);

            Assert.Equal(new Money(20), sut.Charge);
        }

        [Fact]
        public void CreateDailyPeriodWithCorrectAmount()
        {
            var start = new DateTime(2019, 3, 1, 12, 0, 0);
            var end = new DateTime(2019, 3, 5, 16, 0, 0);
            var rate = new BookingRate(new Money(5), new Money(10));

            var sut = BookingPeriod.CreateDailyBooking(start, end, rate);

            Assert.Equal(new Money(40), sut.Charge);
        }

        [Fact]
        public void ReturnTrueIfPeriodsOverlap()
        {
            var period1 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7), money);
            var period2 = new BookingPeriod(new DateTime(2019, 3, 6), new DateTime(2019, 3, 8), money);
            
            var period3 = new BookingPeriod(new DateTime(2019, 3, 6), new DateTime(2019, 3, 14), money);
            var period4 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7), money);
            
            var period5 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7), money);
            var period6 = new BookingPeriod(new DateTime(2019, 3, 3), new DateTime(2019, 3, 4), money);
            
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
            var money = new Money();
            var period1 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7), money);
            var period2 = new BookingPeriod(new DateTime(2019, 3, 8), new DateTime(2019, 3, 9), money);
            
            var period3 = new BookingPeriod(new DateTime(2019, 3, 6), new DateTime(2019, 3, 14), money);
            var period4 = new BookingPeriod(new DateTime(2019, 3, 1), new DateTime(2019, 3, 5), money);
            
            Assert.False(period1.Overlaps(period2));
            Assert.False(period2.Overlaps(period1));
            
            Assert.False(period3.Overlaps(period4));
            Assert.False(period4.Overlaps(period3));
        }
    }
}