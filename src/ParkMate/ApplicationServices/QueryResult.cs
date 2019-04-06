namespace ParkMate.ApplicationServices
{
    public class QueryResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T PayLoad { get; set; }

        public static QueryResult<T> Succeed(T payload)
        {
            return new QueryResult<T>
            {
                PayLoad = payload,
                Success = true
            };
        }
        public static QueryResult<T> Fail(string message)
        {
            return new QueryResult<T>
            {
                Message = message,
                Success = false
            };
        }
    }
}