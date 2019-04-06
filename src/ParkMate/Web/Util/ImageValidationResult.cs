namespace ParkMate.Web.Util
{
    public class ImageValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}