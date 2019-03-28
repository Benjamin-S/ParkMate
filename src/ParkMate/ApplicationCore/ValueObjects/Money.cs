using System;
using System.Collections.Generic;
using System.Globalization;
using ParkMate.ApplicationCore.Exceptions;

namespace ParkMate.ApplicationCore.ValueObjects
{
    public class Money : ValueObject
    {
        public Money() : this(0m)
        {
        }

        public Money(decimal value)
        {
            Validate(value);
            Value = value;
        }

        public decimal Value { get; private set; }
        public CultureInfo CultureInfo { get; } 
                = CultureInfo.CreateSpecificCulture("en-AU");

        private void Validate(decimal value)
        {
            if (value % 0.01m != 0)
            {
                throw new InvalidMoneyValueException(value, 
                    "can not have more than 2 decimals");
            }

            if (value < 0)
            {
                throw new InvalidMoneyValueException(value, 
                    "can not be less than zero");
            }
        }
        private static decimal Round(decimal value)
        {
            return Decimal.Round(value, 2);
        }

        public static Money operator +(Money left, Money right)
        {
            return new Money(left.Value + right.Value);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left.Value - right.Value);
        }

        public static Money operator *(Money left, Money right)
        {
            decimal value = Round(left.Value * right.Value);
            return new Money(value);
        }

        public static Money operator *(Money money, int times)
        {
            decimal value = Round(money.Value * times);
            return new Money(value);
        }
        public static Money operator *(int times, Money money)
        {
            return money * times;
        }
        public override string ToString()
        {
            return Value.ToString("C2", CultureInfo);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return CultureInfo;
        }
    }
}
