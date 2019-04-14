using ApplicationServices.Enums;

namespace ParkMate.ApplicationServices
{
    public struct Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public static Result CommandSuccess(string message)
        {
            return new Result
            {
                Success = true,
                Message = message,
                ResultType = ResultType.Command
            };
        }
        public static Result CommandFail(string message)
        {
            return new Result
            {
                Message = message,
                Success = false,
                ResultType = ResultType.Command
            };
        }
    }

    public class Result<T> 
    {
        public T Payload { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public ResultType ResultType { get; set; }

        public static Result<T> QuerySuccess(T payload)
        {
            return new Result<T>
            {
                Payload = payload,
                Success = true,
                ResultType = ResultType.Query
            };
        }
        public static Result<T> QueryFail(string message)
        {
            return new Result<T>
            {
                Message = message,
                Success = false,
                ResultType = ResultType.Query
            };
        }
       
    }
}