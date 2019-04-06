namespace ParkMate.ApplicationServices
{
    public class QueryResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T PayLoad { get; set; }
    }
}