using System.Net;

namespace Application.Shared.CustomExceptions
{
    public sealed class AppException : ApplicationException
    {
        public AppException(string message, HttpStatusCode code)
        {
            StatusCode = code;
            ErrorMessage = message;
        }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
