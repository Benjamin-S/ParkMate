using System;
namespace ParkMate.ApplicationCore.Exceptions
{
    public class InvalidMoneyValueException : Exception
    {
        public InvalidMoneyValueException(decimal value, string message) :
            base($"Invalid money value: {value}: {message}")
        {
        }
    }
}
