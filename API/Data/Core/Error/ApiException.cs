namespace DatingApp.API.Data.Core.Error
{
    public class ApiException
    {
        public ApiException(int statusCode, string? message = null, string? details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
    }
}
