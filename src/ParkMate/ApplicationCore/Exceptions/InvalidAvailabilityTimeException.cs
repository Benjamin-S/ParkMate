using System;
namespace ParkMate.ApplicationCore.Exceptions
{
    public class InvalidAvailabilityTimeException : Exception
    {
        public InvalidAvailabilityTimeException(
            TimeSpan start, TimeSpan end, string message) 
            : base($"Availability time with {start} - {end} is invalid: {message}")
        {
        }
    }
}
