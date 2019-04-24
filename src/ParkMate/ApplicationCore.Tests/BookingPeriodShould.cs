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

            Assert.Throws<ArgumentException>(() => BookingInfo.CreateHourlyBooking(start, end, new Money()));
        }
        
        [Fact]
        public void ThrowExceptionIfStartEqualsEnd()
        {
            var start = new DateTime(2019, 3, 1);
            var end = new DateTime(2019, 3, 1);

            Assert.Throws<ArgumentException>(() => BookingInfo.CreateHourlyBooking(start, end, new Money())); 
        }

        [Fact]
        public void CreateHourlyPeriodWithCorrectAmount()
        {
            var start = new DateTime(2019, 3, 1, 12, 0, 0);
            var end = new DateTime(2019, 3, 1, 16, 0, 0);
            var rate = new Money(5);

            var sut = BookingInfo.CreateHourlyBooking(start, end, rate);

            Assert.Equal(new Money(20), sut.Total);
        }

        [Fact]
        public void CreateDailyPeriodWithCorrectAmount()
        {
            var start = new DateTime(2019, 3, 1, 12, 0, 0);
            var end = new DateTime(2019, 3, 5, 16, 0, 0);
            var rate = new Money(10);

            var sut = BookingInfo.CreateDailyBooking(start, end, rate);

            Assert.Equal(new Money(40), sut.Total);
        }

        [Fact]
        public void ReturnTrueIfPeriodsOverlap()
        {
            var money = new Money();
            var period1 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7), money);
            var period2 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 6), new DateTime(2019, 3, 8), money);
            
            var period3 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 6), new DateTime(2019, 3, 14), money);
            var period4 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7), money);
            
            var period5 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7), money);
            var period6 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 3), new DateTime(2019, 3, 4), money);
            
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
            var period1 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 1), new DateTime(2019, 3, 7), money);
            var period2 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 8), new DateTime(2019, 3, 9), money);
            
            var period3 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 6), new DateTime(2019, 3, 14), money);
            var period4 = BookingInfo.CreateDailyBooking(new DateTime(2019, 3, 1), new DateTime(2019, 3, 5), money);
            
            Assert.False(period1.Overlaps(period2));
            Assert.False(period2.Overlaps(period1));
            
            Assert.False(period3.Overlaps(period4));
            Assert.False(period4.Overlaps(period3));
        }
    }
}