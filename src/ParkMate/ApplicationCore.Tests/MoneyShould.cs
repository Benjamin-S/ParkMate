using System;
using Xunit;
using ParkMate.ApplicationCore.ValueObjects;
using ParkMate.ApplicationCore.Exceptions;

namespace ApplicationCore.Tests
{
    public class MoneyShould
    {
        [Fact]
        public void ThrowExceptionIfLessThanZero()
        {
            Assert.Throws<InvalidMoneyValueException>(() => new Money(-1));
        }

        [Fact]
        public void ThrowExceptionIfMoreThan2Decimals()
        {
            Assert.Throws<InvalidMoneyValueException>(() => new Money(1.101m));
        }

        [Fact]
        public void SubtractToCorrectAmount()
        {
            var money1 = new Money(7.25m);
            var money2 = new Money(4.75m);

            var result = money1 - money2;

            Assert.Equal(2.5m, result.Value);
        }

        [Fact]
        public void AddToCorrectAmount()
        {
            var money1 = new Money(7.25m);
            var money2 = new Money(4.75m);

            var result = money1 + money2;

            Assert.Equal(12m, result.Value);
        }

        [Fact]
        public void MultiplyByMoneyToCorrectAmount()
        {
            var money1 = new Money(7.25m);
            var money2 = new Money(4.75m);

            var result = money1 * money2;

            Assert.Equal(34.44m, result.Value);
        }

        [Fact]
        public void MultiplyMoneyTimesIntToCorrectAmount()
        {
            var money = new Money(7.29m);
            int times = 17;

            var result = money * times;

            Assert.Equal(123.93m, result.Value);
        }

        [Fact]
        public void MultiplyIntTimesMoneyToCorrectAmount()
        {
            var money = new Money(7.29m);
            int times = 17;

            var result = times * money;

            Assert.Equal(123.93m, result.Value);
        }

        [Fact]
        public void ConvertToCorrectString()
        {
            var money = new Money(75.2m);

            Assert.Equal("$75.20", money.ToString());
        }

        [Fact]
        public void EqualIfValueIsEqual()
        {
            var money1 = new Money(7.25m);
            var money2 = new Money(7.25m);

            Assert.Equal(money1, money2);
        }

        [Fact]
        public void NotEqualIfValueNotEqual()
        {
            var money1 = new Money(7.25m);
            var money2 = new Money(7.26m);

            Assert.NotEqual(money1, money2);
        }
    }
}
