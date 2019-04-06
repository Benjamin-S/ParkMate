namespace ParkMate.ApplicationServices
{
    public class CommandResult
    {
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; }
        public string Message { get; } 
    }
}