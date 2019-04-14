using ApplicationServices.Enums;

namespace ParkMate.ApplicationServices
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public object Payload { get; set; } = new object();
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
        public static Result Default()
        {
            return new Result
            {
                Message = "",
                ResultType = ResultType.None
            };
        }
    }
    public class Result<T> : Result
    {
        public new T Payload { get; set; }

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